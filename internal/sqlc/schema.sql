-- AccountProvider ENUM
DO
$$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'account_provider') THEN
            CREATE TYPE account_provider AS ENUM (
                'Local', 'Google', 'Facebook', 'Microsoft', 'Apple'
                );
        END IF;
    END
$$;

-- UserRole ENUM
DO
$$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_role') THEN
            CREATE TYPE user_role AS ENUM ('Tourist','Guide','Administrator');
        END IF;
    END
$$;

-- TripType ENUM
DO
$$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'trip_type') THEN
            CREATE TYPE trip_type AS ENUM ('Individual','Group','IndividualOrGroup');
        END IF;
    END
$$;

-- TripSearchType ENUM
DO
$$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'trip_search_type') THEN
            CREATE TYPE trip_search_type AS ENUM ('Team','Guide','Sponsor');
        END IF;
    END
$$;


CREATE TABLE IF NOT EXISTS users
(
    id           UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    email        TEXT        NOT NULL,
    is_active    BOOLEAN     NOT NULL,
    account_role TEXT        NOT NULL,
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

CREATE TABLE IF NOT EXISTS accounts
(
    id               UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id          UUID             NOT NULL REFERENCES users (id),
    account_provider account_provider NOT NULL,
    password         TEXT             NOT NULL,
    salt             INTEGER          NOT NULL,
    activation_code  UUID             NOT NULL,
    is_verified      BOOLEAN          NOT NULL,
    last_login_at    TIMESTAMPTZ
);

CREATE INDEX IF NOT EXISTS idx_accounts_user_id ON accounts (user_id);

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
    rating           SMALLINT         NOT NULL,
    likes            INTEGER          NOT NULL,
    trip_type        trip_type        NOT NULL,
    trip_search_type trip_search_type NOT NULL,
    created_at       TIMESTAMPTZ      NOT NULL DEFAULT NOW(),
    updated_at       TIMESTAMPTZ      NOT NULL DEFAULT NOW()
);
CREATE INDEX IF NOT EXISTS idx_trips_user_id ON trips (user_id);

CREATE TABLE IF NOT EXISTS trip_replies
(
    id         UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    trip_id    UUID        NOT NULL REFERENCES trips (id),
    user_id    UUID        NOT NULL REFERENCES users (id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
CREATE INDEX IF NOT EXISTS idx_trip_replies_trip_id ON trip_replies (trip_id);
CREATE INDEX IF NOT EXISTS idx_trip_replies_user_id ON trip_replies (user_id);


CREATE TABLE IF NOT EXISTS trip_reply_messages
(
    id               UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    trip_reply_id    UUID        NOT NULL REFERENCES trip_replies (id),
    user_id          UUID        NOT NULL REFERENCES users (id),
    text             TEXT        NOT NULL,
    images           TEXT[],
    created_datetime TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    is_read          BOOLEAN     NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_trip_reply_messages_reply_id ON trip_reply_messages (trip_reply_id);
CREATE INDEX IF NOT EXISTS idx_trip_reply_messages_user_id ON trip_reply_messages (user_id);