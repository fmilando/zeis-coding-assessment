INSERT INTO "Inventory"("ProductId", "Quantity", "CreatedAt")
SELECT "Id", (floor(random() * 501)::int), NOW()
FROM "Products"
WHERE "Id" NOT IN (SELECT DISTINCT "ProductId" FROM "Inventory");