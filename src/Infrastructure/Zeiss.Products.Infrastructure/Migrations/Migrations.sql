CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260817105239_InitialCreate') THEN
    CREATE TABLE "Inventory" (
        "Id" bigint GENERATED ALWAYS AS IDENTITY,
        "ProductId" bigint NOT NULL,
        "Quantity" integer,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Inventory" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260817105239_InitialCreate') THEN
    CREATE TABLE "Products" (
        "Id" bigint GENERATED ALWAYS AS IDENTITY (START WITH 100000 MAXVALUE 999999),
        "Name" character varying(100) NOT NULL,
        "Sku" character varying(80) NOT NULL,
        "Description" character varying(500),
        "Price" numeric NOT NULL,
        "IsActive" boolean NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "DeletedAt" timestamp with time zone,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260817105239_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260817105239_InitialCreate', '10.0.11');
    END IF;
END $EF$;
COMMIT;

