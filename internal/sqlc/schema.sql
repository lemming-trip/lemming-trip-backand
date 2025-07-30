-- ACCOUNT_PROVIDER ENUM
CREATE TYPE ACCOUNT_PROVIDER AS ENUM ('Local', 'Google', 'Facebook', 'Microsoft', 'Apple');
-- USER_ROLE ENUM
CREATE TYPE USER_ROLE AS ENUM ('Tourist','Guide','Administrator');
-- TRIP_TYPE ENUM
CREATE TYPE TRIP_TYPE AS ENUM ('Individual','Group','IndividualOrGroup');
-- TRIP_SEARCH_TYPE ENUM
CREATE TYPE TRIP_SEARCH_TYPE AS ENUM ('Team','Guide','Sponsor');

-- Create a "users" table
CREATE TABLE IF NOT EXISTS users
(
    id           UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    email        TEXT        NOT NULL,
    is_active    BOOLEAN     NOT NULL,
    user_role    USER_ROLE   NOT NULL,
    avatar       TEXT,
    phone        TEXT,
    city         TEXT,
    address      TEXT,
    first_name   TEXT,
    last_name    TEXT,
    middle_name  TEXT,
    date_birth   TIMESTAMPTZ,
    description  TEXT,
    created_at   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    last_seen_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Create an "accounts" table
CREATE TABLE IF NOT EXISTS accounts
(
    id               UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id          UUID             NOT NULL REFERENCES users (id),
    account_provider ACCOUNT_PROVIDER NOT NULL,
    password         TEXT             NOT NULL,
    salt             INTEGER          NOT NULL,
    activation_code  UUID             NOT NULL,
    is_verified      BOOLEAN          NOT NULL,
    last_login_at    TIMESTAMPTZ
);
CREATE INDEX IF NOT EXISTS idx_accounts_user_id ON accounts (user_id);

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