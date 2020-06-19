.Net Core API for PokeBeer api
Requires a postgres database

-- Table: public."Users"

-- DROP TABLE public."Users";

CREATE TABLE public."Users"
(
    "Id" integer NOT NULL DEFAULT nextval('"Users_Id_seq"'::regclass),
    "FirstName" text COLLATE pg_catalog."default",
    "LastName" text COLLATE pg_catalog."default",
    "PasswordHash" bytea,
    "PasswordSalt" bytea,
    "Username" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Users"
    OWNER to postgres;

-- Table: public."Beers"
-- DROP TABLE public."Beers";

CREATE TABLE public."Beers"
(
    "BeerId" integer NOT NULL DEFAULT nextval('"Beers_BeerId_seq"'::regclass),
    "BeerDescription" text COLLATE pg_catalog."default",
    "BeerImageLink" text COLLATE pg_catalog."default",
    "BeerName" text COLLATE pg_catalog."default",
    "BeerRating" integer,
    "UserId" integer,
    CONSTRAINT "Beer_pkey" PRIMARY KEY ("BeerId"),
    CONSTRAINT "FKBeers_Users" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Beers"
    OWNER to postgres;
-- Index: fki_FKBeers_Users

-- DROP INDEX public."fki_FKBeers_Users";

CREATE INDEX "fki_FKBeers_Users"
    ON public."Beers" USING btree
    ("UserId" ASC NULLS LAST)
    TABLESPACE pg_default;



Implements swagger to view endpoints
{server}/swagger/index.html