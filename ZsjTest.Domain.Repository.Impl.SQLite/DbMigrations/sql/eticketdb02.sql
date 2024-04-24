BEGIN TRANSACTION;

ALTER TABLE "ETickets" ADD "RowVersion" BLOB NOT NULL DEFAULT X'';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240416121410_eticketdb02', '7.0.17');

COMMIT;

