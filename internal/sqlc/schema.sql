-- ACCOUNT_PROVIDER ENUM
CREATE TYPE ACCOUNT_PROVIDER AS ENUM ('local','google','facebook','microsoft','apple','gitHub','discord','telegram','linkedIn','twitter','yandex');
-- USER_ROLE ENUM
CREATE TYPE USER_ROLE AS ENUM ('tourist','guide','administrator');
-- TRIP_TYPE ENUM
CREATE TYPE TRIP_TYPE AS ENUM ('individual','group','individual_or_group');
-- TRIP_SEARCH_TYPE ENUM
CREATE TYPE TRIP_SEARCH_TYPE AS ENUM ('team','guide','sponsor');
-- SECURITY_EVENT_TYPE ENUM
CREATE TYPE SECURITY_EVENT_TYPE AS ENUM ('login','logout','password_change', 'mfa_enabled');
-- SECURITY_EVENT_STATUS ENUM
CREATE TYPE SECURITY_EVENT_STATUS AS ENUM ('success', 'failure');
-- GDPR_CONSENT_TYPE ENUM
CREATE TYPE GDPR_CONSENT_TYPE AS ENUM ('marketing', 'analytics', 'cookies', 'other');

-- Create a "users" table
CREATE TABLE IF NOT EXISTS users
(
    id               UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    email            TEXT        NOT NULL,
    is_active        BOOLEAN     NOT NULL,
    user_role        USER_ROLE   NOT NULL,
    avatar           TEXT,
    phone            TEXT,
    city             TEXT,
    address          TEXT,
    first_name       TEXT,
    last_name        TEXT,
    middle_name      TEXT,
    date_birth       TIMESTAMPTZ,
    description      TEXT,
    privacy_settings JSONB                DEFAULT '{
      "profile_visible": true,
      "email_visible": false
    }',
    preferences      JSONB                DEFAULT '{}',
    ban              BOOLEAN     NOT NULL DEFAULT FALSE,
    ban_reason       TEXT,
    created_at       TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at       TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    last_seen_at     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    ban_at           TIMESTAMPTZ
);

-- Create an "accounts" table
CREATE TABLE IF NOT EXISTS accounts
(
    id                    UUID PRIMARY KEY          DEFAULT gen_random_uuid(),
    user_id               UUID             NOT NULL REFERENCES users (id),
    account_provider      ACCOUNT_PROVIDER NOT NULL,
    provider_user_id      TEXT,   -- User's ID from provider
    porvider_email        TEXT,   -- User's Email from provider (it's different from default email)

    -- Fields for local auth (nullable for OAuth)
    password_hash         TEXT,
    password_salt         TEXT,

    -- Verification and activation
    activation_code       UUID,
    activation_expires_at TIMESTAMPTZ,
    is_verified           BOOLEAN          NOT NULL DEFAULT FALSE,
    verified_at           TIMESTAMPTZ,

    -- MFA (Multi-Factor Authentication)
    mfa_enabled           BOOLEAN          NOT NULL DEFAULT FALSE,
    mfa_secret            TEXT,   -- For TOTP
    mfa_backup_codes      TEXT[], -- Reserve codes

    -- Security
    failed_login_attempts INTEGER          NOT NULL DEFAULT 0,
    locked_until          TIMESTAMPTZ,
    last_login_at         TIMESTAMPTZ,
    last_login_ip         INET,

    -- Metadata
    created_at            TIMESTAMPTZ      NOT NULL DEFAULT NOW(),
    updated_at            TIMESTAMPTZ      NOT NULL DEFAULT NOW(),

    -- Constraints
    CONSTRAINT password_required_for_local
        CHECK (account_provider != 'local' OR password_hash IS NOT NULL),
    CONSTRAINT provider_id_required_for_oauth
        CHECK (account_provider = 'local' OR provider_user_id IS NOT NULL)

);
CREATE INDEX idx_accounts_user_id ON accounts (user_id);
CREATE INDEX idx_accounts_provider_user ON accounts (account_provider, provider_user_id);
CREATE UNIQUE INDEX idx_accounts_email_provider ON accounts (porvider_email, account_provider) WHERE porvider_email IS NOT NULL;

-- Table for OAuth tokens
CREATE TABLE IF NOT EXISTS oauth_tokens
(
    id            UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    account_id    UUID        NOT NULL REFERENCES accounts (id) ON DELETE CASCADE,
    access_token  TEXT        NOT NULL,
    refresh_token TEXT,
    token_type    TEXT        NOT NULL DEFAULT 'Bearer',
    scope         TEXT[],
    expires_at    TIMESTAMPTZ NOT NULL,
    created_at    TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    revoked_at    TIMESTAMPTZ
);

CREATE INDEX idx_oauth_tokens_account_id ON oauth_tokens (account_id);
CREATE INDEX idx_oauth_tokens_expires_at ON oauth_tokens (expires_at)
    WHERE revoked_at IS NULL;

-- Table for sessions
CREATE TABLE IF NOT EXISTS user_sessions
(
    id            UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    account_id    UUID        NOT NULL REFERENCES accounts (id) ON DELETE CASCADE,
    session_token TEXT        NOT NULL UNIQUE,
    ip_address    INET,
    user_agent    TEXT,
    device_info   JSONB,
    expires_at    TIMESTAMPTZ NOT NULL,
    last_activity TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_at    TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    revoked_at    TIMESTAMPTZ,
    is_active     BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE INDEX idx_user_sessions_token ON user_sessions (session_token)
    WHERE revoked_at IS NULL;
CREATE INDEX idx_user_sessions_account_id ON user_sessions (account_id)
    WHERE revoked_at IS NULL;
CREATE INDEX idx_user_sessions_expires_at ON user_sessions (expires_at)
    WHERE revoked_at IS NULL;

-- Table for security audit
CREATE TABLE IF NOT EXISTS security_audit_log
(
    id           UUID PRIMARY KEY               DEFAULT gen_random_uuid(),
    account_id   UUID                  REFERENCES accounts (id) ON DELETE SET NULL,
    event_type   SECURITY_EVENT_TYPE   NOT NULL,
    event_status SECURITY_EVENT_STATUS NOT NULL,
    ip_address   INET,
    user_agent   TEXT,
    details      JSONB,
    created_at   TIMESTAMPTZ           NOT NULL DEFAULT NOW()
);

CREATE INDEX idx_security_audit_account_id ON security_audit_log (account_id);
CREATE INDEX idx_security_audit_created_at ON security_audit_log (created_at);
CREATE INDEX idx_security_audit_event_type ON security_audit_log (event_type);

-- Table for GDPR
CREATE TABLE IF NOT EXISTS gdpr_consents
(
    id           UUID PRIMARY KEY           DEFAULT gen_random_uuid(),
    user_id      UUID              NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    consent_type GDPR_CONSENT_TYPE NOT NULL,
    is_granted   BOOLEAN           NOT NULL,
    granted_at   TIMESTAMPTZ,
    revoked_at   TIMESTAMPTZ,
    ip_address   INET,
    created_at   TIMESTAMPTZ       NOT NULL DEFAULT NOW()
);

CREATE INDEX idx_gdpr_consents_user_id ON gdpr_consents (user_id);
CREATE INDEX idx_gdpr_consents_type ON gdpr_consents (consent_type);

-- Create a "trips" table
CREATE TABLE IF NOT EXISTS trips
(
    id               UUID PRIMARY KEY          DEFAULT gen_random_uuid(),
    user_id          UUID             NOT NULL REFERENCES users (id),
    title            TEXT             NOT NULL,
    text             TEXT,
    title_image      TEXT,
    images           TEXT[],
    video_link       TEXT,
    route            JSONB            NOT NULL,
    rating           SMALLINT         NOT NULL DEFAULT 0,
    likes            INTEGER          NOT NULL DEFAULT 0,
    trip_type        TRIP_TYPE        NOT NULL,
    trip_search_type TRIP_SEARCH_TYPE NOT NULL,
    created_at       TIMESTAMPTZ      NOT NULL DEFAULT NOW(),
    updated_at       TIMESTAMPTZ      NOT NULL DEFAULT NOW()
);
CREATE INDEX IF NOT EXISTS idx_trips_user_id ON trips (user_id);

-- Create a "trip_comments" table
CREATE TABLE IF NOT EXISTS trip_comments
(
    id         UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    trip_id    UUID        NOT NULL REFERENCES trips (id) ON DELETE CASCADE,
    user_id    UUID        NOT NULL REFERENCES users (id),
    parent_id  UUID REFERENCES trip_comments (id),
    text       TEXT        NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    is_read    BOOLEAN     NOT NULL DEFAULT FALSE,
    is_deleted BOOLEAN     NOT NULL DEFAULT FALSE,
    CHECK (parent_id IS NULL OR parent_id <> id)
);
CREATE INDEX IF NOT EXISTS idx_trip_comments_trip_id ON trip_comments (trip_id);
CREATE INDEX IF NOT EXISTS idx_trip_comments_user_id ON trip_comments (user_id);
CREATE INDEX IF NOT EXISTS idx_trip_comments_parent_id ON trip_comments (parent_id);

-- Create a "trip_replies" table
CREATE TABLE IF NOT EXISTS trip_replies
(
    id         UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    trip_id    UUID        NOT NULL REFERENCES trips (id),
    user_id    UUID        NOT NULL REFERENCES users (id),
    text       TEXT        NOT NULL,
    images     TEXT[],
    is_read    BOOLEAN     NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
CREATE INDEX IF NOT EXISTS idx_trip_replies_trip_id ON trip_replies (trip_id);
CREATE INDEX IF NOT EXISTS idx_trip_replies_user_id ON trip_replies (user_id);

-- Create a "messages" table
CREATE TABLE IF NOT EXISTS messages
(
    id           UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    sender_id    UUID        NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    recipient_id UUID        NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    content      TEXT        NOT NULL,
    created_at   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    is_read      BOOLEAN     NOT NULL DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_messages_sender_id ON messages (sender_id);
CREATE INDEX IF NOT EXISTS idx_messages_recipient_id ON messages (recipient_id);