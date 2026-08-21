-- delete all data from the table
TRUNCATE TABLE "Products";

-- seed for products table --
INSERT INTO "Products"("Name", "Sku", "Description", "Price", "IsActive", "IsDeleted", "CreatedAt")
SELECT 'Athletic Ankle Socks', 'ATHL-0001', '6-pack moisture-wicking cushioned socks', 18.8, TRUE, FALSE, NOW()
UNION
SELECT 'Storage Bin Set', 'STOR-0002', 'Set of 6 stackable fabric storage bins with lids', 34.27, TRUE, FALSE, NOW()
UNION
SELECT 'Whiteboard Magnetic 24x36', 'WHIT-0003', 'Dry erase board with aluminum frame and marker tray', 47.22, TRUE, FALSE, NOW()
UNION
SELECT 'USB Hub 7-Port', 'USBH-0004', 'Powered USB 3.0 hub with individual switches', 23.76, TRUE, FALSE, NOW()
UNION
SELECT 'Non-Stick Frying Pan 10-inch', 'NONS-0005', 'Ceramic coated frying pan with heat-resistant handle', 31.44, TRUE, FALSE, NOW()
UNION
SELECT 'Memory Foam Pillow', 'MEMO-0006', 'Contoured cervical pillow for neck support', 30.68, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0007', 'Mesh back chair with lumbar support and adjustable arms', 150.5, TRUE, FALSE, NOW()
UNION
SELECT 'Document Shredder', 'DOCU-0008', '6-sheet cross-cut shredder with 4-gallon bin', 40.78, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Slim Fit Jeans', 'MENS-0009', 'Stretch denim jeans in dark wash', 31.59, TRUE, FALSE, NOW()
UNION
SELECT 'Ceramic Coffee Mug Set', 'CERA-0010', 'Set of 4 12oz mugs with matching saucers', 19.32, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0011', 'High-speed HDMI 2.1 cable supporting 8K resolution', 7.6, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Polo Shirt', 'MENS-0012', 'Classic fit pique polo shirt, 100% cotton', 21.33, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0013', 'High-waisted leggings with moisture-wicking fabric', 29.53, TRUE, FALSE, NOW()
UNION
SELECT 'Foam Roller', 'FOAM-0014', 'High-density muscle recovery foam roller, 18 inches', 20.64, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Cotton T-Shirt', 'MENS-0015', 'Crew neck short sleeve tee, available in multiple colors', 12.95, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0016', 'Adjustable ventilated helmet with LED safety light', 29.18, TRUE, FALSE, NOW()
UNION
SELECT 'Mechanical Gaming Keyboard', 'MECH-0017', 'RGB backlit keyboard with hot-swappable switches', 78.68, TRUE, FALSE, NOW()
UNION
SELECT 'Cotton Bed Sheet Set', 'COTT-0018', 'Queen size 4-piece sheet set, 100% cotton', 58.41, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mechanical Keyboard', 'WIRE-0019', 'Compact 75% layout keyboard with hot-swap switches', 75.91, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mouse', 'WIRE-0020', 'Ergonomic wireless mouse with adjustable DPI settings', 24.13, TRUE, FALSE, NOW()
UNION
SELECT 'Digital Kitchen Scale', 'DIGI-0021', 'Precision scale with tare function, up to 11lbs', 13.36, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Earbuds', 'WIRE-0022', 'True wireless earbuds with charging case and touch controls', 67.8, TRUE, FALSE, NOW()
UNION
SELECT 'Desktop File Organizer', 'DESK-0023', '3-tier stackable paper tray organizer', 17.46, TRUE, FALSE, NOW()
UNION
SELECT 'LED Desk Lamp', 'LEDD-0024', 'Dimmable lamp with USB charging port and 3 color modes', 25.68, TRUE, FALSE, NOW()
UNION
SELECT 'Adjustable Dumbbell Set', 'ADJU-0025', '5-25lb adjustable dumbbells, pair', 149.36, TRUE, FALSE, NOW()
UNION
SELECT 'Kitchen Knife Set', 'KITC-0026', '5-piece stainless steel knife set with wooden block', 59.19, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Bluetooth Headphones', 'WIRE-0027', 'Over-ear headphones with active noise cancellation and 30-hour battery life', 118.98, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0028', '20L leak-proof cooler backpack for outdoor trips', 48.68, TRUE, FALSE, NOW()
UNION
SELECT 'Scented Soy Candle', 'SCEN-0029', 'Hand-poured candle with 40-hour burn time', 16.73, TRUE, FALSE, NOW()
UNION
SELECT 'Notebook Set Ruled', 'NOTE-0030', 'Pack of 3 hardcover notebooks, 120 pages each', 17.42, TRUE, FALSE, NOW()
UNION
SELECT 'Bamboo Cutting Board Set', 'BAMB-0031', 'Set of 3 eco-friendly cutting boards', 22.28, TRUE, FALSE, NOW()
UNION
SELECT 'Glass Food Storage Containers', 'GLAS-0032', 'Set of 10 airtight glass containers with lids', 30.47, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Winter Parka', 'MENS-0033', 'Waterproof insulated jacket with faux fur hood', 95.76, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0034', 'Digital air fryer with 8 preset cooking modes', 68.02, TRUE, FALSE, NOW()
UNION
SELECT 'Sleeping Bag 3-Season', 'SLEE-0035', 'Compact mummy sleeping bag rated to 20°F', 45.26, TRUE, FALSE, NOW()
UNION
SELECT 'Phone Tripod Stand', 'PHON-0036', 'Flexible tripod with phone clip and remote shutter', 19.59, TRUE, FALSE, NOW()
UNION
SELECT 'Wool Blend Beanie', 'WOOL-0037', 'Warm knit beanie hat, one size fits most', 14.25, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Tent 4-Person', 'CAMP-0038', 'Waterproof dome tent with easy setup', 82.58, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Hooded Sweatshirt', 'UNIS-0039', 'Fleece-lined pullover hoodie with kangaroo pocket', 34.82, TRUE, FALSE, NOW()
UNION
SELECT 'Gel Pens Set', 'GELP-0040', '24-pack assorted color gel pens, fine point', 12.36, TRUE, FALSE, NOW()
UNION
SELECT 'External SSD 1TB', 'EXTE-0041', 'Portable solid state drive with USB-C connector', 116.57, TRUE, FALSE, NOW()
UNION
SELECT 'Hiking Backpack 40L', 'HIKI-0042', 'Lightweight water-resistant backpack with multiple compartments', 58.76, TRUE, FALSE, NOW()
UNION
SELECT 'Sticky Notes Bundle', 'STIC-0043', '12-pack assorted sizes and colors sticky notes', 8.31, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Power Bank 20000mAh', 'PORT-0044', 'Dual USB output power bank with LED charge indicator', 28.69, TRUE, FALSE, NOW()
UNION
SELECT 'Throw Blanket Fleece', 'THRO-0045', 'Soft plush blanket, 50x60 inches', 23.6, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Denim Jacket', 'WOME-0046', 'Classic cropped denim jacket with button closure', 40.24, TRUE, FALSE, NOW()
UNION
SELECT '4K Webcam', 'KWEB-0047', '1080p/4K webcam with built-in microphone for streaming', 69.22, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Running Shoes', 'WOME-0048', 'Lightweight breathable sneakers with cushioned sole', 76.41, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Organizer Set', 'DESK-0049', '5-piece mesh desk organizer for office supplies', 18.98, TRUE, FALSE, NOW()
UNION
SELECT 'Resistance Bands Set', 'RESI-0050', '5 bands with varying resistance levels and door anchor', 17.18, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Speaker', 'BLUE-0051', 'Waterproof portable speaker with 12-hour playtime', 59.92, TRUE, FALSE, NOW()
UNION
SELECT 'Standing Desk Converter', 'STAN-0052', 'Height-adjustable desktop riser for dual monitors', 120.56, TRUE, FALSE, NOW()
UNION
SELECT 'Laptop Stand Aluminum', 'LAPT-0053', 'Adjustable ergonomic stand compatible with laptops up to 17 inches', 21.35, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Charging Pad', 'WIRE-0054', '10W fast wireless charger compatible with Qi devices', 16.41, TRUE, FALSE, NOW()
UNION
SELECT 'Stainless Steel Water Bottle', 'STAI-0055', 'Insulated bottle keeps drinks cold for 24 hours', 16.09, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Kettle 1.7L', 'ELEC-0056', 'Rapid boil kettle with auto shut-off feature', 34.4, TRUE, FALSE, NOW()
UNION
SELECT 'Jump Rope Speed', 'JUMP-0057', 'Adjustable steel cable jump rope with ball bearings', 13.74, TRUE, FALSE, NOW()
UNION
SELECT 'USB-C Fast Charging Cable', 'USBC-0058', '6ft braided nylon cable supporting 60W fast charging and data transfer', 11.52, TRUE, FALSE, NOW()
UNION
SELECT 'Smartwatch Fitness Tracker', 'SMAR-0059', 'Heart rate monitor and step counter with 7-day battery', 53.17, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Mat Non-Slip', 'YOGA-0060', '6mm thick eco-friendly yoga mat with carrying strap', 23.81, TRUE, FALSE, NOW()
UNION
SELECT 'Athletic Ankle Socks', 'ATHL-0061', '6-pack moisture-wicking cushioned socks', 19.96, TRUE, FALSE, NOW()
UNION
SELECT 'Storage Bin Set', 'STOR-0062', 'Set of 6 stackable fabric storage bins with lids', 32.93, TRUE, FALSE, NOW()
UNION
SELECT 'Whiteboard Magnetic 24x36', 'WHIT-0063', 'Dry erase board with aluminum frame and marker tray', 49.41, TRUE, FALSE, NOW()
UNION
SELECT 'USB Hub 7-Port', 'USBH-0064', 'Powered USB 3.0 hub with individual switches', 26.6, TRUE, FALSE, NOW()
UNION
SELECT 'Non-Stick Frying Pan 10-inch', 'NONS-0065', 'Ceramic coated frying pan with heat-resistant handle', 23.13, TRUE, FALSE, NOW()
UNION
SELECT 'Memory Foam Pillow', 'MEMO-0066', 'Contoured cervical pillow for neck support', 40.8, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0067', 'Mesh back chair with lumbar support and adjustable arms', 191.34, TRUE, FALSE, NOW()
UNION
SELECT 'Document Shredder', 'DOCU-0068', '6-sheet cross-cut shredder with 4-gallon bin', 45.73, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Slim Fit Jeans', 'MENS-0069', 'Stretch denim jeans in dark wash', 35.33, TRUE, FALSE, NOW()
UNION
SELECT 'Ceramic Coffee Mug Set', 'CERA-0070', 'Set of 4 12oz mugs with matching saucers', 23.4, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0071', 'High-speed HDMI 2.1 cable supporting 8K resolution', 7.66, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Polo Shirt', 'MENS-0072', 'Classic fit pique polo shirt, 100% cotton', 23.21, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0073', 'High-waisted leggings with moisture-wicking fabric', 26.8, TRUE, FALSE, NOW()
UNION
SELECT 'Foam Roller', 'FOAM-0074', 'High-density muscle recovery foam roller, 18 inches', 26.53, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Cotton T-Shirt', 'MENS-0075', 'Crew neck short sleeve tee, available in multiple colors', 17.0, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0076', 'Adjustable ventilated helmet with LED safety light', 30.26, TRUE, FALSE, NOW()
UNION
SELECT 'Mechanical Gaming Keyboard', 'MECH-0077', 'RGB backlit keyboard with hot-swappable switches', 95.03, TRUE, FALSE, NOW()
UNION
SELECT 'Cotton Bed Sheet Set', 'COTT-0078', 'Queen size 4-piece sheet set, 100% cotton', 39.46, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mechanical Keyboard', 'WIRE-0079', 'Compact 75% layout keyboard with hot-swap switches', 86.5, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mouse', 'WIRE-0080', 'Ergonomic wireless mouse with adjustable DPI settings', 28.05, TRUE, FALSE, NOW()
UNION
SELECT 'Digital Kitchen Scale', 'DIGI-0081', 'Precision scale with tare function, up to 11lbs', 14.38, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Earbuds', 'WIRE-0082', 'True wireless earbuds with charging case and touch controls', 63.74, TRUE, FALSE, NOW()
UNION
SELECT 'Desktop File Organizer', 'DESK-0083', '3-tier stackable paper tray organizer', 21.47, TRUE, FALSE, NOW()
UNION
SELECT 'LED Desk Lamp', 'LEDD-0084', 'Dimmable lamp with USB charging port and 3 color modes', 22.28, TRUE, FALSE, NOW()
UNION
SELECT 'Adjustable Dumbbell Set', 'ADJU-0085', '5-25lb adjustable dumbbells, pair', 135.74, TRUE, FALSE, NOW()
UNION
SELECT 'Kitchen Knife Set', 'KITC-0086', '5-piece stainless steel knife set with wooden block', 56.17, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Bluetooth Headphones', 'WIRE-0087', 'Over-ear headphones with active noise cancellation and 30-hour battery life', 134.49, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0088', '20L leak-proof cooler backpack for outdoor trips', 45.6, TRUE, FALSE, NOW()
UNION
SELECT 'Scented Soy Candle', 'SCEN-0089', 'Hand-poured candle with 40-hour burn time', 9.99, TRUE, FALSE, NOW()
UNION
SELECT 'Notebook Set Ruled', 'NOTE-0090', 'Pack of 3 hardcover notebooks, 120 pages each', 14.26, TRUE, FALSE, NOW()
UNION
SELECT 'Bamboo Cutting Board Set', 'BAMB-0091', 'Set of 3 eco-friendly cutting boards', 20.18, TRUE, FALSE, NOW()
UNION
SELECT 'Glass Food Storage Containers', 'GLAS-0092', 'Set of 10 airtight glass containers with lids', 43.93, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Winter Parka', 'MENS-0093', 'Waterproof insulated jacket with faux fur hood', 123.93, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0094', 'Digital air fryer with 8 preset cooking modes', 84.94, TRUE, FALSE, NOW()
UNION
SELECT 'Sleeping Bag 3-Season', 'SLEE-0095', 'Compact mummy sleeping bag rated to 20°F', 47.68, TRUE, FALSE, NOW()
UNION
SELECT 'Phone Tripod Stand', 'PHON-0096', 'Flexible tripod with phone clip and remote shutter', 13.4, TRUE, FALSE, NOW()
UNION
SELECT 'Wool Blend Beanie', 'WOOL-0097', 'Warm knit beanie hat, one size fits most', 14.26, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Tent 4-Person', 'CAMP-0098', 'Waterproof dome tent with easy setup', 107.87, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Hooded Sweatshirt', 'UNIS-0099', 'Fleece-lined pullover hoodie with kangaroo pocket', 26.27, TRUE, FALSE, NOW()
UNION
SELECT 'Gel Pens Set', 'GELP-0100', '24-pack assorted color gel pens, fine point', 12.91, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0101', 'High-speed HDMI 2.1 cable supporting 8K resolution', 12.16, TRUE, FALSE, NOW()
UNION
SELECT 'Smartwatch Fitness Tracker', 'SMAR-0102', 'Heart rate monitor and step counter with 7-day battery', 57.63, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0103', 'Mesh back chair with lumbar support and adjustable arms', 144.39, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0104', 'High-waisted leggings with moisture-wicking fabric', 30.2, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Activewear Sports Bra', 'WOME-0105', 'Medium support bra with removable padding', 20.95, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0106', '20L leak-proof cooler backpack for outdoor trips', 42.69, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0107', 'Digital air fryer with 8 preset cooking modes', 77.87, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Car Adapter', 'BLUE-0108', 'FM transmitter with hands-free calling and USB charging', 17.67, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0109', 'Adjustable ventilated helmet with LED safety light', 30.02, TRUE, FALSE, NOW()
UNION
SELECT 'Smartphone Gimbal Stabilizer', 'SMAR-0110', '3-axis gimbal for smooth video recording', 103.18, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Hooded Sweatshirt', 'UNIS-0111', 'Fleece-lined pullover hoodie with kangaroo pocket', 39.13, TRUE, FALSE, NOW()
UNION
SELECT 'Gel Pens Set', 'GELP-0112', '24-pack assorted color gel pens, fine point', 14.07, TRUE, FALSE, NOW()
UNION
SELECT 'Basketball Official Size', 'BASK-0113', 'Indoor/outdoor composite leather basketball', 21.71, TRUE, FALSE, NOW()
UNION
SELECT 'LED Desk Lamp', 'LEDD-0114', 'Dimmable lamp with USB charging port and 3 color modes', 33.26, TRUE, FALSE, NOW()
UNION
SELECT 'Kids'' Graphic T-Shirt', 'KIDS-0115', 'Soft cotton tee with printed design', 12.5, TRUE, FALSE, NOW()
UNION
SELECT 'Desktop File Organizer', 'DESK-0116', '3-tier stackable paper tray organizer', 22.91, TRUE, FALSE, NOW()
UNION
SELECT 'Water Resistant Duffel Bag', 'WATE-0117', '40L sports duffel with shoe compartment', 36.79, TRUE, FALSE, NOW()
UNION
SELECT 'Nail Care Kit', 'NAIL-0118', '12-piece manicure and pedicure set with case', 15.04, TRUE, FALSE, NOW()
UNION
SELECT 'Memory Foam Pillow', 'MEMO-0119', 'Contoured cervical pillow for neck support', 32.36, TRUE, FALSE, NOW()
UNION
SELECT 'Coffee Maker Drip 12-Cup', 'COFF-0120', 'Programmable drip coffee maker with glass carafe', 35.07, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Polo Shirt', 'MENS-0121', 'Classic fit pique polo shirt, 100% cotton', 26.66, TRUE, FALSE, NOW()
UNION
SELECT 'Running Belt Waist Pack', 'RUNN-0122', 'Adjustable waist pack for phone and essentials', 18.03, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Keyboard and Mouse Combo', 'WIRE-0123', 'Slim wireless combo with quiet keys', 44.43, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Athletic Shorts', 'MENS-0124', 'Breathable mesh shorts with elastic waistband', 22.61, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Power Bank 20000mAh', 'PORT-0125', 'Dual USB output power bank with LED charge indicator', 32.61, TRUE, FALSE, NOW()
UNION
SELECT 'Hiking Backpack 40L', 'HIKI-0126', 'Lightweight water-resistant backpack with multiple compartments', 48.18, TRUE, FALSE, NOW()
UNION
SELECT 'Binder Clips Assorted', 'BIND-0127', '60-pack assorted size binder clips', 8.49, TRUE, FALSE, NOW()
UNION
SELECT 'Digital Kitchen Scale', 'DIGI-0128', 'Precision scale with tare function, up to 11lbs', 18.72, TRUE, FALSE, NOW()
UNION
SELECT 'Filing Cabinet 2-Drawer', 'FILI-0129', 'Metal filing cabinet with lock', 95.37, TRUE, FALSE, NOW()
UNION
SELECT 'USB-C Fast Charging Cable', 'USBC-0130', '6ft braided nylon cable supporting 60W fast charging and data transfer', 10.18, TRUE, FALSE, NOW()
UNION
SELECT 'Non-Stick Frying Pan 10-inch', 'NONS-0131', 'Ceramic coated frying pan with heat-resistant handle', 27.48, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Tent 4-Person', 'CAMP-0132', 'Waterproof dome tent with easy setup', 76.45, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Calendar 2026', 'DESK-0133', 'Monthly planner desk calendar with stand', 14.71, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Toothbrush', 'ELEC-0134', 'Rechargeable sonic toothbrush with 3 cleaning modes', 43.44, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Charging Pad', 'WIRE-0135', '10W fast wireless charger compatible with Qi devices', 24.26, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mechanical Keyboard', 'WIRE-0136', 'Compact 75% layout keyboard with hot-swap switches', 73.95, TRUE, FALSE, NOW()
UNION
SELECT 'Essential Oil Diffuser', 'ESSE-0137', 'Ultrasonic diffuser with 7 LED light colors', 23.34, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Summer Dress', 'WOME-0138', 'Lightweight floral print sundress', 27.23, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Chair Folding', 'CAMP-0139', 'Portable folding chair with cup holder and carry bag', 30.43, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Cotton T-Shirt', 'MENS-0140', 'Crew neck short sleeve tee, available in multiple colors', 17.87, TRUE, FALSE, NOW()
UNION
SELECT 'Sticky Notes Bundle', 'STIC-0141', '12-pack assorted sizes and colors sticky notes', 11.02, TRUE, FALSE, NOW()
UNION
SELECT 'Kitchen Knife Set', 'KITC-0142', '5-piece stainless steel knife set with wooden block', 47.17, TRUE, FALSE, NOW()
UNION
SELECT 'Notebook Set Ruled', 'NOTE-0143', 'Pack of 3 hardcover notebooks, 120 pages each', 13.68, TRUE, FALSE, NOW()
UNION
SELECT 'Document Shredder', 'DOCU-0144', '6-sheet cross-cut shredder with 4-gallon bin', 46.34, TRUE, FALSE, NOW()
UNION
SELECT 'Glass Food Storage Containers', 'GLAS-0145', 'Set of 10 airtight glass containers with lids', 31.17, TRUE, FALSE, NOW()
UNION
SELECT 'Water Gun Blaster', 'WATE-0146', 'High-capacity water blaster for outdoor play', 15.85, TRUE, FALSE, NOW()
UNION
SELECT 'Toaster 4-Slice', 'TOAS-0147', 'Extra-wide slot toaster with 7 browning settings', 46.31, TRUE, FALSE, NOW()
UNION
SELECT 'Kayak Paddle', 'KAYA-0148', 'Lightweight aluminum kayak paddle, adjustable length', 64.44, TRUE, FALSE, NOW()
UNION
SELECT 'Fishing Rod and Reel Combo', 'FISH-0149', 'Telescopic rod with spinning reel, travel-friendly', 43.31, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Winter Parka', 'MENS-0150', 'Waterproof insulated jacket with faux fur hood', 86.28, TRUE, FALSE, NOW()
UNION
SELECT 'Bath Towel Set', 'BATH-0151', '6-piece cotton towel set, quick-dry fabric', 34.89, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Dryer Ionic', 'HAIR-0152', '1875W ionic dryer with diffuser attachment', 48.93, TRUE, FALSE, NOW()
UNION
SELECT 'Ring Light 10-inch', 'RING-0153', 'LED ring light with tripod stand and phone holder', 22.47, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Cardigan Sweater', 'WOME-0154', 'Open-front knit cardigan with pockets', 40.54, TRUE, FALSE, NOW()
UNION
SELECT 'Corkboard Bulletin Board', 'CORK-0155', '24x18 framed corkboard with push pins', 19.06, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Gaming Controller', 'WIRE-0156', 'Bluetooth controller compatible with PC and mobile', 54.1, TRUE, FALSE, NOW()
UNION
SELECT 'Printer All-in-One', 'PRIN-0157', 'Wireless inkjet printer with scan and copy', 126.44, TRUE, FALSE, NOW()
UNION
SELECT 'Blender High Speed', 'BLEN-0158', '1000W blender with 6 stainless steel blades', 71.42, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Dress Shirt', 'MENS-0159', 'Wrinkle-resistant button-down dress shirt', 38.79, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Shaver Men''s', 'ELEC-0160', 'Rotary shaver with pop-up trimmer, cordless', 52.81, TRUE, FALSE, NOW()
UNION
SELECT 'Label Maker Portable', 'LABE-0161', 'Handheld label maker with QWERTY keyboard', 25.97, TRUE, FALSE, NOW()
UNION
SELECT 'Bathroom Scale Digital', 'BATH-0162', 'Smart scale with body fat and BMI tracking', 30.07, TRUE, FALSE, NOW()
UNION
SELECT 'Wool Blend Beanie', 'WOOL-0163', 'Warm knit beanie hat, one size fits most', 11.23, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Swim Trunks', 'MENS-0164', 'Quick-dry swim trunks with mesh lining', 26.99, TRUE, FALSE, NOW()
UNION
SELECT 'Resistance Bands Set', 'RESI-0165', '5 bands with varying resistance levels and door anchor', 19.5, TRUE, FALSE, NOW()
UNION
SELECT 'Calculator Scientific', 'CALC-0166', 'Handheld scientific calculator with 240 functions', 14.73, TRUE, FALSE, NOW()
UNION
SELECT 'Building Blocks Set 500pc', 'BUIL-0167', 'Compatible interlocking building bricks with storage box', 25.95, TRUE, FALSE, NOW()
UNION
SELECT 'Sleeping Bag 3-Season', 'SLEE-0168', 'Compact mummy sleeping bag rated to 20°F', 40.52, TRUE, FALSE, NOW()
UNION
SELECT 'Laptop Stand Aluminum', 'LAPT-0169', 'Adjustable ergonomic stand compatible with laptops up to 17 inches', 28.3, TRUE, FALSE, NOW()
UNION
SELECT 'Streaming Microphone USB', 'STRE-0170', 'Condenser microphone with pop filter for podcasting', 55.59, TRUE, FALSE, NOW()
UNION
SELECT 'Wall Clock Modern', 'WALL-0171', '12-inch silent non-ticking wall clock', 13.06, TRUE, FALSE, NOW()
UNION
SELECT 'Throw Blanket Fleece', 'THRO-0172', 'Soft plush blanket, 50x60 inches', 25.07, TRUE, FALSE, NOW()
UNION
SELECT 'Body Lotion Set', 'BODY-0173', '3-pack moisturizing lotion, various scents', 15.46, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Earbuds', 'WIRE-0174', 'True wireless earbuds with charging case and touch controls', 38.02, TRUE, FALSE, NOW()
UNION
SELECT 'Golf Balls 12-Pack', 'GOLF-0175', 'Distance golf balls with durable cover', 15.3, TRUE, FALSE, NOW()
UNION
SELECT 'Noise Cancelling Earbuds', 'NOIS-0176', 'In-ear buds with active noise cancellation and 24-hour case battery', 79.82, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Baseball Cap', 'UNIS-0177', 'Adjustable cotton cap with curved brim', 13.59, TRUE, FALSE, NOW()
UNION
SELECT 'Athletic Ankle Socks', 'ATHL-0178', '6-pack moisture-wicking cushioned socks', 14.94, TRUE, FALSE, NOW()
UNION
SELECT '4K Webcam', 'KWEB-0179', '1080p/4K webcam with built-in microphone for streaming', 64.26, TRUE, FALSE, NOW()
UNION
SELECT 'Storage Bin Set', 'STOR-0180', 'Set of 6 stackable fabric storage bins with lids', 33.08, TRUE, FALSE, NOW()
UNION
SELECT 'Paper Shredder Basket', 'PAPE-0181', 'Compact home shredder for small offices', 35.84, TRUE, FALSE, NOW()
UNION
SELECT 'Robot Vacuum Cleaner', 'ROBO-0182', 'Smart robot vacuum with app control and auto-charging', 238.23, TRUE, FALSE, NOW()
UNION
SELECT 'Shower Curtain Set', 'SHOW-0183', 'Waterproof curtain with matching hooks', 20.75, TRUE, FALSE, NOW()
UNION
SELECT 'Phone Tripod Stand', 'PHON-0184', 'Flexible tripod with phone clip and remote shutter', 14.69, TRUE, FALSE, NOW()
UNION
SELECT 'Laundry Hamper Collapsible', 'LAUN-0185', 'Foldable fabric hamper with carry handles', 19.72, TRUE, FALSE, NOW()
UNION
SELECT 'Remote Control Car', 'REMO-0186', '4WD off-road RC car with rechargeable battery', 40.17, TRUE, FALSE, NOW()
UNION
SELECT 'Facial Cleansing Brush', 'FACI-0187', 'Silicone facial brush with waterproof design', 21.4, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Presenter Remote', 'WIRE-0188', 'PowerPoint clicker with laser pointer', 21.58, TRUE, FALSE, NOW()
UNION
SELECT 'Board Game Family Night', 'BOAR-0189', 'Strategy board game for 2-6 players, ages 8+', 25.3, TRUE, FALSE, NOW()
UNION
SELECT 'Smart Plug WiFi', 'SMAR-0190', 'App-controlled smart plug compatible with voice assistants', 12.87, TRUE, FALSE, NOW()
UNION
SELECT 'Standing Desk Converter', 'STAN-0191', 'Height-adjustable desktop riser for dual monitors', 141.82, TRUE, FALSE, NOW()
UNION
SELECT 'Area Rug 5x7', 'AREA-0192', 'Soft pile area rug for living room', 52.16, TRUE, FALSE, NOW()
UNION
SELECT 'Card Game Party Pack', 'CARD-0193', 'Fast-paced card game for family game nights', 14.56, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Pad Large', 'DESK-0194', 'Non-slip PU leather desk mat, 31x15 inches', 21.45, TRUE, FALSE, NOW()
UNION
SELECT 'Action Camera 4K', 'ACTI-0195', 'Waterproof action camera with mounting accessories kit', 53.02, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Bluetooth Headphones', 'WIRE-0196', 'Over-ear headphones with active noise cancellation and 30-hour battery life', 130.96, TRUE, FALSE, NOW()
UNION
SELECT 'Scented Soy Candle', 'SCEN-0197', 'Hand-poured candle with 40-hour burn time', 16.39, TRUE, FALSE, NOW()
UNION
SELECT 'Drone with Camera', 'DRON-0198', 'Beginner-friendly drone with HD camera and app control', 55.45, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Denim Jacket', 'WOME-0199', 'Classic cropped denim jacket with button closure', 38.82, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Straightener Ceramic', 'HAIR-0200', 'Dual voltage flat iron with adjustable heat', 28.03, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Slim Fit Jeans', 'MENS-0201', 'Stretch denim jeans in dark wash', 32.79, TRUE, FALSE, NOW()
UNION
SELECT 'Wooden Train Set', 'WOOD-0202', 'Classic wooden track set with 40 pieces', 28.66, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Camping Stove', 'PORT-0203', 'Compact propane stove for outdoor cooking', 34.24, TRUE, FALSE, NOW()
UNION
SELECT 'Trekking Poles Pair', 'TREK-0204', 'Collapsible aluminum trekking poles with cork grips', 38.11, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Organizer Set', 'DESK-0205', '5-piece mesh desk organizer for office supplies', 15.74, TRUE, FALSE, NOW()
UNION
SELECT 'Stainless Steel Water Bottle', 'STAI-0206', 'Insulated bottle keeps drinks cold for 24 hours', 23.07, TRUE, FALSE, NOW()
UNION
SELECT 'Adjustable Dumbbell Set', 'ADJU-0207', '5-25lb adjustable dumbbells, pair', 141.35, TRUE, FALSE, NOW()
UNION
SELECT 'Puzzle 1000 Piece', 'PUZZ-0208', 'Landscape jigsaw puzzle for adults', 13.68, TRUE, FALSE, NOW()
UNION
SELECT 'Jump Rope Speed', 'JUMP-0209', 'Adjustable steel cable jump rope with ball bearings', 12.9, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Ankle Boots', 'WOME-0210', 'Faux leather boots with side zipper', 56.21, TRUE, FALSE, NOW()
UNION
SELECT 'Ceramic Coffee Mug Set', 'CERA-0211', 'Set of 4 12oz mugs with matching saucers', 17.14, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Kettle 1.7L', 'ELEC-0212', 'Rapid boil kettle with auto shut-off feature', 26.39, TRUE, FALSE, NOW()
UNION
SELECT 'Art Supplies Kit', 'ARTS-0213', '50-piece kids art set with case', 22.53, TRUE, FALSE, NOW()
UNION
SELECT 'Mechanical Gaming Keyboard', 'MECH-0214', 'RGB backlit keyboard with hot-swappable switches', 76.54, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mouse', 'WIRE-0215', 'Ergonomic wireless mouse with adjustable DPI settings', 21.09, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Running Shoes', 'WOME-0216', 'Lightweight breathable sneakers with cushioned sole', 61.84, TRUE, FALSE, NOW()
UNION
SELECT 'Foam Roller', 'FOAM-0217', 'High-density muscle recovery foam roller, 18 inches', 25.64, TRUE, FALSE, NOW()
UNION
SELECT 'USB Hub 7-Port', 'USBH-0218', 'Powered USB 3.0 hub with individual switches', 27.01, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Leather Belt', 'MENS-0219', 'Genuine leather belt with metal buckle', 16.64, TRUE, FALSE, NOW()
UNION
SELECT 'Plush Stuffed Animal', 'PLUS-0220', 'Soft plush toy, 14 inches, machine washable', 12.01, TRUE, FALSE, NOW()
UNION
SELECT 'Makeup Brush Set', 'MAKE-0221', '12-piece professional makeup brush set with case', 18.89, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Block Set', 'YOGA-0222', '2-pack high-density foam yoga blocks', 16.48, TRUE, FALSE, NOW()
UNION
SELECT 'External SSD 1TB', 'EXTE-0223', 'Portable solid state drive with USB-C connector', 111.4, TRUE, FALSE, NOW()
UNION
SELECT 'Whiteboard Magnetic 24x36', 'WHIT-0224', 'Dry erase board with aluminum frame and marker tray', 35.69, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Monitor 15.6-inch', 'PORT-0225', 'Full HD USB-C portable monitor with built-in speakers', 178.75, TRUE, FALSE, NOW()
UNION
SELECT 'Massage Gun Percussion', 'MASS-0226', 'Deep tissue massager with 6 attachment heads', 79.21, TRUE, FALSE, NOW()
UNION
SELECT 'Cotton Bed Sheet Set', 'COTT-0227', 'Queen size 4-piece sheet set, 100% cotton', 54.57, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Mat Non-Slip', 'YOGA-0228', '6mm thick eco-friendly yoga mat with carrying strap', 26.61, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Speaker', 'BLUE-0229', 'Waterproof portable speaker with 12-hour playtime', 44.59, TRUE, FALSE, NOW()
UNION
SELECT 'Bamboo Cutting Board Set', 'BAMB-0230', 'Set of 3 eco-friendly cutting boards', 21.89, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0231', 'High-speed HDMI 2.1 cable supporting 8K resolution', 8.3, TRUE, FALSE, NOW()
UNION
SELECT 'Smartwatch Fitness Tracker', 'SMAR-0232', 'Heart rate monitor and step counter with 7-day battery', 52.91, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0233', 'Mesh back chair with lumbar support and adjustable arms', 196.21, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0234', 'High-waisted leggings with moisture-wicking fabric', 20.9, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Activewear Sports Bra', 'WOME-0235', 'Medium support bra with removable padding', 18.13, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0236', '20L leak-proof cooler backpack for outdoor trips', 35.99, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0237', 'Digital air fryer with 8 preset cooking modes', 74.29, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Car Adapter', 'BLUE-0238', 'FM transmitter with hands-free calling and USB charging', 22.18, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0239', 'Adjustable ventilated helmet with LED safety light', 35.61, TRUE, FALSE, NOW()
UNION
SELECT 'Smartphone Gimbal Stabilizer', 'SMAR-0240', '3-axis gimbal for smooth video recording', 73.4, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Hooded Sweatshirt', 'UNIS-0241', 'Fleece-lined pullover hoodie with kangaroo pocket', 32.61, TRUE, FALSE, NOW()
UNION
SELECT 'Gel Pens Set', 'GELP-0242', '24-pack assorted color gel pens, fine point', 15.1, TRUE, FALSE, NOW()
UNION
SELECT 'Basketball Official Size', 'BASK-0243', 'Indoor/outdoor composite leather basketball', 21.02, TRUE, FALSE, NOW()
UNION
SELECT 'LED Desk Lamp', 'LEDD-0244', 'Dimmable lamp with USB charging port and 3 color modes', 21.01, TRUE, FALSE, NOW()
UNION
SELECT 'Kids'' Graphic T-Shirt', 'KIDS-0245', 'Soft cotton tee with printed design', 13.16, TRUE, FALSE, NOW()
UNION
SELECT 'Desktop File Organizer', 'DESK-0246', '3-tier stackable paper tray organizer', 19.62, TRUE, FALSE, NOW()
UNION
SELECT 'Water Resistant Duffel Bag', 'WATE-0247', '40L sports duffel with shoe compartment', 48.82, TRUE, FALSE, NOW()
UNION
SELECT 'Nail Care Kit', 'NAIL-0248', '12-piece manicure and pedicure set with case', 16.98, TRUE, FALSE, NOW()
UNION
SELECT 'Memory Foam Pillow', 'MEMO-0249', 'Contoured cervical pillow for neck support', 38.67, TRUE, FALSE, NOW()
UNION
SELECT 'Coffee Maker Drip 12-Cup', 'COFF-0250', 'Programmable drip coffee maker with glass carafe', 35.98, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Polo Shirt', 'MENS-0251', 'Classic fit pique polo shirt, 100% cotton', 18.97, TRUE, FALSE, NOW()
UNION
SELECT 'Running Belt Waist Pack', 'RUNN-0252', 'Adjustable waist pack for phone and essentials', 17.59, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Keyboard and Mouse Combo', 'WIRE-0253', 'Slim wireless combo with quiet keys', 36.29, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Athletic Shorts', 'MENS-0254', 'Breathable mesh shorts with elastic waistband', 18.15, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Power Bank 20000mAh', 'PORT-0255', 'Dual USB output power bank with LED charge indicator', 28.9, TRUE, FALSE, NOW()
UNION
SELECT 'Hiking Backpack 40L', 'HIKI-0256', 'Lightweight water-resistant backpack with multiple compartments', 65.08, TRUE, FALSE, NOW()
UNION
SELECT 'Binder Clips Assorted', 'BIND-0257', '60-pack assorted size binder clips', 7.25, TRUE, FALSE, NOW()
UNION
SELECT 'Digital Kitchen Scale', 'DIGI-0258', 'Precision scale with tare function, up to 11lbs', 14.11, TRUE, FALSE, NOW()
UNION
SELECT 'Filing Cabinet 2-Drawer', 'FILI-0259', 'Metal filing cabinet with lock', 76.53, TRUE, FALSE, NOW()
UNION
SELECT 'USB-C Fast Charging Cable', 'USBC-0260', '6ft braided nylon cable supporting 60W fast charging and data transfer', 12.86, TRUE, FALSE, NOW()
UNION
SELECT 'Non-Stick Frying Pan 10-inch', 'NONS-0261', 'Ceramic coated frying pan with heat-resistant handle', 28.48, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Tent 4-Person', 'CAMP-0262', 'Waterproof dome tent with easy setup', 107.15, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Calendar 2026', 'DESK-0263', 'Monthly planner desk calendar with stand', 14.6, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Toothbrush', 'ELEC-0264', 'Rechargeable sonic toothbrush with 3 cleaning modes', 25.18, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Charging Pad', 'WIRE-0265', '10W fast wireless charger compatible with Qi devices', 21.58, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mechanical Keyboard', 'WIRE-0266', 'Compact 75% layout keyboard with hot-swap switches', 72.51, TRUE, FALSE, NOW()
UNION
SELECT 'Essential Oil Diffuser', 'ESSE-0267', 'Ultrasonic diffuser with 7 LED light colors', 18.29, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Summer Dress', 'WOME-0268', 'Lightweight floral print sundress', 35.74, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Chair Folding', 'CAMP-0269', 'Portable folding chair with cup holder and carry bag', 32.58, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Cotton T-Shirt', 'MENS-0270', 'Crew neck short sleeve tee, available in multiple colors', 11.05, TRUE, FALSE, NOW()
UNION
SELECT 'Sticky Notes Bundle', 'STIC-0271', '12-pack assorted sizes and colors sticky notes', 8.74, TRUE, FALSE, NOW()
UNION
SELECT 'Kitchen Knife Set', 'KITC-0272', '5-piece stainless steel knife set with wooden block', 42.05, TRUE, FALSE, NOW()
UNION
SELECT 'Notebook Set Ruled', 'NOTE-0273', 'Pack of 3 hardcover notebooks, 120 pages each', 13.7, TRUE, FALSE, NOW()
UNION
SELECT 'Document Shredder', 'DOCU-0274', '6-sheet cross-cut shredder with 4-gallon bin', 40.69, TRUE, FALSE, NOW()
UNION
SELECT 'Glass Food Storage Containers', 'GLAS-0275', 'Set of 10 airtight glass containers with lids', 36.56, TRUE, FALSE, NOW()
UNION
SELECT 'Water Gun Blaster', 'WATE-0276', 'High-capacity water blaster for outdoor play', 14.34, TRUE, FALSE, NOW()
UNION
SELECT 'Toaster 4-Slice', 'TOAS-0277', 'Extra-wide slot toaster with 7 browning settings', 36.04, TRUE, FALSE, NOW()
UNION
SELECT 'Kayak Paddle', 'KAYA-0278', 'Lightweight aluminum kayak paddle, adjustable length', 64.59, TRUE, FALSE, NOW()
UNION
SELECT 'Fishing Rod and Reel Combo', 'FISH-0279', 'Telescopic rod with spinning reel, travel-friendly', 50.17, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Winter Parka', 'MENS-0280', 'Waterproof insulated jacket with faux fur hood', 106.44, TRUE, FALSE, NOW()
UNION
SELECT 'Bath Towel Set', 'BATH-0281', '6-piece cotton towel set, quick-dry fabric', 35.01, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Dryer Ionic', 'HAIR-0282', '1875W ionic dryer with diffuser attachment', 41.08, TRUE, FALSE, NOW()
UNION
SELECT 'Ring Light 10-inch', 'RING-0283', 'LED ring light with tripod stand and phone holder', 33.97, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Cardigan Sweater', 'WOME-0284', 'Open-front knit cardigan with pockets', 32.06, TRUE, FALSE, NOW()
UNION
SELECT 'Corkboard Bulletin Board', 'CORK-0285', '24x18 framed corkboard with push pins', 21.77, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Gaming Controller', 'WIRE-0286', 'Bluetooth controller compatible with PC and mobile', 36.6, TRUE, FALSE, NOW()
UNION
SELECT 'Printer All-in-One', 'PRIN-0287', 'Wireless inkjet printer with scan and copy', 124.48, TRUE, FALSE, NOW()
UNION
SELECT 'Blender High Speed', 'BLEN-0288', '1000W blender with 6 stainless steel blades', 70.97, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Dress Shirt', 'MENS-0289', 'Wrinkle-resistant button-down dress shirt', 28.1, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Shaver Men''s', 'ELEC-0290', 'Rotary shaver with pop-up trimmer, cordless', 42.03, TRUE, FALSE, NOW()
UNION
SELECT 'Label Maker Portable', 'LABE-0291', 'Handheld label maker with QWERTY keyboard', 23.15, TRUE, FALSE, NOW()
UNION
SELECT 'Bathroom Scale Digital', 'BATH-0292', 'Smart scale with body fat and BMI tracking', 25.13, TRUE, FALSE, NOW()
UNION
SELECT 'Wool Blend Beanie', 'WOOL-0293', 'Warm knit beanie hat, one size fits most', 13.11, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Swim Trunks', 'MENS-0294', 'Quick-dry swim trunks with mesh lining', 26.52, TRUE, FALSE, NOW()
UNION
SELECT 'Resistance Bands Set', 'RESI-0295', '5 bands with varying resistance levels and door anchor', 20.04, TRUE, FALSE, NOW()
UNION
SELECT 'Calculator Scientific', 'CALC-0296', 'Handheld scientific calculator with 240 functions', 14.75, TRUE, FALSE, NOW()
UNION
SELECT 'Building Blocks Set 500pc', 'BUIL-0297', 'Compatible interlocking building bricks with storage box', 38.61, TRUE, FALSE, NOW()
UNION
SELECT 'Sleeping Bag 3-Season', 'SLEE-0298', 'Compact mummy sleeping bag rated to 20°F', 41.26, TRUE, FALSE, NOW()
UNION
SELECT 'Laptop Stand Aluminum', 'LAPT-0299', 'Adjustable ergonomic stand compatible with laptops up to 17 inches', 29.5, TRUE, FALSE, NOW()
UNION
SELECT 'Streaming Microphone USB', 'STRE-0300', 'Condenser microphone with pop filter for podcasting', 64.02, TRUE, FALSE, NOW()
UNION
SELECT 'Wall Clock Modern', 'WALL-0301', '12-inch silent non-ticking wall clock', 13.43, TRUE, FALSE, NOW()
UNION
SELECT 'Throw Blanket Fleece', 'THRO-0302', 'Soft plush blanket, 50x60 inches', 21.33, TRUE, FALSE, NOW()
UNION
SELECT 'Body Lotion Set', 'BODY-0303', '3-pack moisturizing lotion, various scents', 16.04, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Earbuds', 'WIRE-0304', 'True wireless earbuds with charging case and touch controls', 79.08, TRUE, FALSE, NOW()
UNION
SELECT 'Golf Balls 12-Pack', 'GOLF-0305', 'Distance golf balls with durable cover', 16.61, TRUE, FALSE, NOW()
UNION
SELECT 'Noise Cancelling Earbuds', 'NOIS-0306', 'In-ear buds with active noise cancellation and 24-hour case battery', 86.5, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Baseball Cap', 'UNIS-0307', 'Adjustable cotton cap with curved brim', 14.93, TRUE, FALSE, NOW()
UNION
SELECT 'Athletic Ankle Socks', 'ATHL-0308', '6-pack moisture-wicking cushioned socks', 16.92, TRUE, FALSE, NOW()
UNION
SELECT '4K Webcam', 'KWEB-0309', '1080p/4K webcam with built-in microphone for streaming', 45.58, TRUE, FALSE, NOW()
UNION
SELECT 'Storage Bin Set', 'STOR-0310', 'Set of 6 stackable fabric storage bins with lids', 39.17, TRUE, FALSE, NOW()
UNION
SELECT 'Paper Shredder Basket', 'PAPE-0311', 'Compact home shredder for small offices', 35.36, TRUE, FALSE, NOW()
UNION
SELECT 'Robot Vacuum Cleaner', 'ROBO-0312', 'Smart robot vacuum with app control and auto-charging', 164.9, TRUE, FALSE, NOW()
UNION
SELECT 'Shower Curtain Set', 'SHOW-0313', 'Waterproof curtain with matching hooks', 15.35, TRUE, FALSE, NOW()
UNION
SELECT 'Phone Tripod Stand', 'PHON-0314', 'Flexible tripod with phone clip and remote shutter', 15.57, TRUE, FALSE, NOW()
UNION
SELECT 'Laundry Hamper Collapsible', 'LAUN-0315', 'Foldable fabric hamper with carry handles', 20.52, TRUE, FALSE, NOW()
UNION
SELECT 'Remote Control Car', 'REMO-0316', '4WD off-road RC car with rechargeable battery', 40.73, TRUE, FALSE, NOW()
UNION
SELECT 'Facial Cleansing Brush', 'FACI-0317', 'Silicone facial brush with waterproof design', 20.62, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Presenter Remote', 'WIRE-0318', 'PowerPoint clicker with laser pointer', 18.64, TRUE, FALSE, NOW()
UNION
SELECT 'Board Game Family Night', 'BOAR-0319', 'Strategy board game for 2-6 players, ages 8+', 33.99, TRUE, FALSE, NOW()
UNION
SELECT 'Smart Plug WiFi', 'SMAR-0320', 'App-controlled smart plug compatible with voice assistants', 16.8, TRUE, FALSE, NOW()
UNION
SELECT 'Standing Desk Converter', 'STAN-0321', 'Height-adjustable desktop riser for dual monitors', 92.38, TRUE, FALSE, NOW()
UNION
SELECT 'Area Rug 5x7', 'AREA-0322', 'Soft pile area rug for living room', 64.3, TRUE, FALSE, NOW()
UNION
SELECT 'Card Game Party Pack', 'CARD-0323', 'Fast-paced card game for family game nights', 14.76, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Pad Large', 'DESK-0324', 'Non-slip PU leather desk mat, 31x15 inches', 21.66, TRUE, FALSE, NOW()
UNION
SELECT 'Action Camera 4K', 'ACTI-0325', 'Waterproof action camera with mounting accessories kit', 67.67, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Bluetooth Headphones', 'WIRE-0326', 'Over-ear headphones with active noise cancellation and 30-hour battery life', 119.18, TRUE, FALSE, NOW()
UNION
SELECT 'Scented Soy Candle', 'SCEN-0327', 'Hand-poured candle with 40-hour burn time', 16.99, TRUE, FALSE, NOW()
UNION
SELECT 'Drone with Camera', 'DRON-0328', 'Beginner-friendly drone with HD camera and app control', 98.68, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Denim Jacket', 'WOME-0329', 'Classic cropped denim jacket with button closure', 49.98, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Straightener Ceramic', 'HAIR-0330', 'Dual voltage flat iron with adjustable heat', 33.88, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Slim Fit Jeans', 'MENS-0331', 'Stretch denim jeans in dark wash', 34.72, TRUE, FALSE, NOW()
UNION
SELECT 'Wooden Train Set', 'WOOD-0332', 'Classic wooden track set with 40 pieces', 28.24, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Camping Stove', 'PORT-0333', 'Compact propane stove for outdoor cooking', 40.99, TRUE, FALSE, NOW()
UNION
SELECT 'Trekking Poles Pair', 'TREK-0334', 'Collapsible aluminum trekking poles with cork grips', 27.65, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Organizer Set', 'DESK-0335', '5-piece mesh desk organizer for office supplies', 19.11, TRUE, FALSE, NOW()
UNION
SELECT 'Stainless Steel Water Bottle', 'STAI-0336', 'Insulated bottle keeps drinks cold for 24 hours', 16.78, TRUE, FALSE, NOW()
UNION
SELECT 'Adjustable Dumbbell Set', 'ADJU-0337', '5-25lb adjustable dumbbells, pair', 145.46, TRUE, FALSE, NOW()
UNION
SELECT 'Puzzle 1000 Piece', 'PUZZ-0338', 'Landscape jigsaw puzzle for adults', 18.47, TRUE, FALSE, NOW()
UNION
SELECT 'Jump Rope Speed', 'JUMP-0339', 'Adjustable steel cable jump rope with ball bearings', 11.46, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Ankle Boots', 'WOME-0340', 'Faux leather boots with side zipper', 60.09, TRUE, FALSE, NOW()
UNION
SELECT 'Ceramic Coffee Mug Set', 'CERA-0341', 'Set of 4 12oz mugs with matching saucers', 24.34, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Kettle 1.7L', 'ELEC-0342', 'Rapid boil kettle with auto shut-off feature', 28.71, TRUE, FALSE, NOW()
UNION
SELECT 'Art Supplies Kit', 'ARTS-0343', '50-piece kids art set with case', 16.58, TRUE, FALSE, NOW()
UNION
SELECT 'Mechanical Gaming Keyboard', 'MECH-0344', 'RGB backlit keyboard with hot-swappable switches', 109.08, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mouse', 'WIRE-0345', 'Ergonomic wireless mouse with adjustable DPI settings', 20.73, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Running Shoes', 'WOME-0346', 'Lightweight breathable sneakers with cushioned sole', 46.35, TRUE, FALSE, NOW()
UNION
SELECT 'Foam Roller', 'FOAM-0347', 'High-density muscle recovery foam roller, 18 inches', 21.7, TRUE, FALSE, NOW()
UNION
SELECT 'USB Hub 7-Port', 'USBH-0348', 'Powered USB 3.0 hub with individual switches', 19.99, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Leather Belt', 'MENS-0349', 'Genuine leather belt with metal buckle', 24.17, TRUE, FALSE, NOW()
UNION
SELECT 'Plush Stuffed Animal', 'PLUS-0350', 'Soft plush toy, 14 inches, machine washable', 14.79, TRUE, FALSE, NOW()
UNION
SELECT 'Makeup Brush Set', 'MAKE-0351', '12-piece professional makeup brush set with case', 23.2, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Block Set', 'YOGA-0352', '2-pack high-density foam yoga blocks', 16.09, TRUE, FALSE, NOW()
UNION
SELECT 'External SSD 1TB', 'EXTE-0353', 'Portable solid state drive with USB-C connector', 88.91, TRUE, FALSE, NOW()
UNION
SELECT 'Whiteboard Magnetic 24x36', 'WHIT-0354', 'Dry erase board with aluminum frame and marker tray', 43.19, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Monitor 15.6-inch', 'PORT-0355', 'Full HD USB-C portable monitor with built-in speakers', 157.88, TRUE, FALSE, NOW()
UNION
SELECT 'Massage Gun Percussion', 'MASS-0356', 'Deep tissue massager with 6 attachment heads', 61.13, TRUE, FALSE, NOW()
UNION
SELECT 'Cotton Bed Sheet Set', 'COTT-0357', 'Queen size 4-piece sheet set, 100% cotton', 36.73, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Mat Non-Slip', 'YOGA-0358', '6mm thick eco-friendly yoga mat with carrying strap', 27.72, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Speaker', 'BLUE-0359', 'Waterproof portable speaker with 12-hour playtime', 40.52, TRUE, FALSE, NOW()
UNION
SELECT 'Bamboo Cutting Board Set', 'BAMB-0360', 'Set of 3 eco-friendly cutting boards', 25.08, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0361', 'High-speed HDMI 2.1 cable supporting 8K resolution', 11.07, TRUE, FALSE, NOW()
UNION
SELECT 'Smartwatch Fitness Tracker', 'SMAR-0362', 'Heart rate monitor and step counter with 7-day battery', 92.17, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0363', 'Mesh back chair with lumbar support and adjustable arms', 159.79, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0364', 'High-waisted leggings with moisture-wicking fabric', 20.4, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Activewear Sports Bra', 'WOME-0365', 'Medium support bra with removable padding', 23.76, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0366', '20L leak-proof cooler backpack for outdoor trips', 40.21, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0367', 'Digital air fryer with 8 preset cooking modes', 77.41, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Car Adapter', 'BLUE-0368', 'FM transmitter with hands-free calling and USB charging', 22.83, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0369', 'Adjustable ventilated helmet with LED safety light', 25.76, TRUE, FALSE, NOW()
UNION
SELECT 'Smartphone Gimbal Stabilizer', 'SMAR-0370', '3-axis gimbal for smooth video recording', 105.78, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Hooded Sweatshirt', 'UNIS-0371', 'Fleece-lined pullover hoodie with kangaroo pocket', 30.18, TRUE, FALSE, NOW()
UNION
SELECT 'Gel Pens Set', 'GELP-0372', '24-pack assorted color gel pens, fine point', 14.71, TRUE, FALSE, NOW()
UNION
SELECT 'Basketball Official Size', 'BASK-0373', 'Indoor/outdoor composite leather basketball', 26.54, TRUE, FALSE, NOW()
UNION
SELECT 'LED Desk Lamp', 'LEDD-0374', 'Dimmable lamp with USB charging port and 3 color modes', 34.75, TRUE, FALSE, NOW()
UNION
SELECT 'Kids'' Graphic T-Shirt', 'KIDS-0375', 'Soft cotton tee with printed design', 8.68, TRUE, FALSE, NOW()
UNION
SELECT 'Desktop File Organizer', 'DESK-0376', '3-tier stackable paper tray organizer', 24.09, TRUE, FALSE, NOW()
UNION
SELECT 'Water Resistant Duffel Bag', 'WATE-0377', '40L sports duffel with shoe compartment', 33.79, TRUE, FALSE, NOW()
UNION
SELECT 'Nail Care Kit', 'NAIL-0378', '12-piece manicure and pedicure set with case', 13.3, TRUE, FALSE, NOW()
UNION
SELECT 'Memory Foam Pillow', 'MEMO-0379', 'Contoured cervical pillow for neck support', 36.53, TRUE, FALSE, NOW()
UNION
SELECT 'Coffee Maker Drip 12-Cup', 'COFF-0380', 'Programmable drip coffee maker with glass carafe', 47.99, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Polo Shirt', 'MENS-0381', 'Classic fit pique polo shirt, 100% cotton', 27.67, TRUE, FALSE, NOW()
UNION
SELECT 'Running Belt Waist Pack', 'RUNN-0382', 'Adjustable waist pack for phone and essentials', 17.8, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Keyboard and Mouse Combo', 'WIRE-0383', 'Slim wireless combo with quiet keys', 43.8, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Athletic Shorts', 'MENS-0384', 'Breathable mesh shorts with elastic waistband', 22.36, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Power Bank 20000mAh', 'PORT-0385', 'Dual USB output power bank with LED charge indicator', 27.95, TRUE, FALSE, NOW()
UNION
SELECT 'Hiking Backpack 40L', 'HIKI-0386', 'Lightweight water-resistant backpack with multiple compartments', 57.93, TRUE, FALSE, NOW()
UNION
SELECT 'Binder Clips Assorted', 'BIND-0387', '60-pack assorted size binder clips', 9.79, TRUE, FALSE, NOW()
UNION
SELECT 'Digital Kitchen Scale', 'DIGI-0388', 'Precision scale with tare function, up to 11lbs', 19.36, TRUE, FALSE, NOW()
UNION
SELECT 'Filing Cabinet 2-Drawer', 'FILI-0389', 'Metal filing cabinet with lock', 101.15, TRUE, FALSE, NOW()
UNION
SELECT 'USB-C Fast Charging Cable', 'USBC-0390', '6ft braided nylon cable supporting 60W fast charging and data transfer', 12.97, TRUE, FALSE, NOW()
UNION
SELECT 'Non-Stick Frying Pan 10-inch', 'NONS-0391', 'Ceramic coated frying pan with heat-resistant handle', 24.49, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Tent 4-Person', 'CAMP-0392', 'Waterproof dome tent with easy setup', 105.99, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Calendar 2026', 'DESK-0393', 'Monthly planner desk calendar with stand', 12.03, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Toothbrush', 'ELEC-0394', 'Rechargeable sonic toothbrush with 3 cleaning modes', 38.33, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Charging Pad', 'WIRE-0395', '10W fast wireless charger compatible with Qi devices', 18.93, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mechanical Keyboard', 'WIRE-0396', 'Compact 75% layout keyboard with hot-swap switches', 77.88, TRUE, FALSE, NOW()
UNION
SELECT 'Essential Oil Diffuser', 'ESSE-0397', 'Ultrasonic diffuser with 7 LED light colors', 24.2, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Summer Dress', 'WOME-0398', 'Lightweight floral print sundress', 28.83, TRUE, FALSE, NOW()
UNION
SELECT 'Camping Chair Folding', 'CAMP-0399', 'Portable folding chair with cup holder and carry bag', 34.96, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Cotton T-Shirt', 'MENS-0400', 'Crew neck short sleeve tee, available in multiple colors', 13.02, TRUE, FALSE, NOW()
UNION
SELECT 'Sticky Notes Bundle', 'STIC-0401', '12-pack assorted sizes and colors sticky notes', 10.73, TRUE, FALSE, NOW()
UNION
SELECT 'Kitchen Knife Set', 'KITC-0402', '5-piece stainless steel knife set with wooden block', 45.21, TRUE, FALSE, NOW()
UNION
SELECT 'Notebook Set Ruled', 'NOTE-0403', 'Pack of 3 hardcover notebooks, 120 pages each', 15.97, TRUE, FALSE, NOW()
UNION
SELECT 'Document Shredder', 'DOCU-0404', '6-sheet cross-cut shredder with 4-gallon bin', 43.11, TRUE, FALSE, NOW()
UNION
SELECT 'Glass Food Storage Containers', 'GLAS-0405', 'Set of 10 airtight glass containers with lids', 42.5, TRUE, FALSE, NOW()
UNION
SELECT 'Water Gun Blaster', 'WATE-0406', 'High-capacity water blaster for outdoor play', 12.42, TRUE, FALSE, NOW()
UNION
SELECT 'Toaster 4-Slice', 'TOAS-0407', 'Extra-wide slot toaster with 7 browning settings', 34.19, TRUE, FALSE, NOW()
UNION
SELECT 'Kayak Paddle', 'KAYA-0408', 'Lightweight aluminum kayak paddle, adjustable length', 59.63, TRUE, FALSE, NOW()
UNION
SELECT 'Fishing Rod and Reel Combo', 'FISH-0409', 'Telescopic rod with spinning reel, travel-friendly', 45.16, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Winter Parka', 'MENS-0410', 'Waterproof insulated jacket with faux fur hood', 96.1, TRUE, FALSE, NOW()
UNION
SELECT 'Bath Towel Set', 'BATH-0411', '6-piece cotton towel set, quick-dry fabric', 31.62, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Dryer Ionic', 'HAIR-0412', '1875W ionic dryer with diffuser attachment', 43.5, TRUE, FALSE, NOW()
UNION
SELECT 'Ring Light 10-inch', 'RING-0413', 'LED ring light with tripod stand and phone holder', 27.66, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Cardigan Sweater', 'WOME-0414', 'Open-front knit cardigan with pockets', 45.86, TRUE, FALSE, NOW()
UNION
SELECT 'Corkboard Bulletin Board', 'CORK-0415', '24x18 framed corkboard with push pins', 22.59, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Gaming Controller', 'WIRE-0416', 'Bluetooth controller compatible with PC and mobile', 48.39, TRUE, FALSE, NOW()
UNION
SELECT 'Printer All-in-One', 'PRIN-0417', 'Wireless inkjet printer with scan and copy', 112.93, TRUE, FALSE, NOW()
UNION
SELECT 'Blender High Speed', 'BLEN-0418', '1000W blender with 6 stainless steel blades', 54.92, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Dress Shirt', 'MENS-0419', 'Wrinkle-resistant button-down dress shirt', 38.27, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Shaver Men''s', 'ELEC-0420', 'Rotary shaver with pop-up trimmer, cordless', 50.47, TRUE, FALSE, NOW()
UNION
SELECT 'Label Maker Portable', 'LABE-0421', 'Handheld label maker with QWERTY keyboard', 21.39, TRUE, FALSE, NOW()
UNION
SELECT 'Bathroom Scale Digital', 'BATH-0422', 'Smart scale with body fat and BMI tracking', 34.27, TRUE, FALSE, NOW()
UNION
SELECT 'Wool Blend Beanie', 'WOOL-0423', 'Warm knit beanie hat, one size fits most', 10.4, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Swim Trunks', 'MENS-0424', 'Quick-dry swim trunks with mesh lining', 21.09, TRUE, FALSE, NOW()
UNION
SELECT 'Resistance Bands Set', 'RESI-0425', '5 bands with varying resistance levels and door anchor', 23.06, TRUE, FALSE, NOW()
UNION
SELECT 'Calculator Scientific', 'CALC-0426', 'Handheld scientific calculator with 240 functions', 14.02, TRUE, FALSE, NOW()
UNION
SELECT 'Building Blocks Set 500pc', 'BUIL-0427', 'Compatible interlocking building bricks with storage box', 25.68, TRUE, FALSE, NOW()
UNION
SELECT 'Sleeping Bag 3-Season', 'SLEE-0428', 'Compact mummy sleeping bag rated to 20°F', 64.59, TRUE, FALSE, NOW()
UNION
SELECT 'Laptop Stand Aluminum', 'LAPT-0429', 'Adjustable ergonomic stand compatible with laptops up to 17 inches', 29.16, TRUE, FALSE, NOW()
UNION
SELECT 'Streaming Microphone USB', 'STRE-0430', 'Condenser microphone with pop filter for podcasting', 61.89, TRUE, FALSE, NOW()
UNION
SELECT 'Wall Clock Modern', 'WALL-0431', '12-inch silent non-ticking wall clock', 17.54, TRUE, FALSE, NOW()
UNION
SELECT 'Throw Blanket Fleece', 'THRO-0432', 'Soft plush blanket, 50x60 inches', 26.85, TRUE, FALSE, NOW()
UNION
SELECT 'Body Lotion Set', 'BODY-0433', '3-pack moisturizing lotion, various scents', 19.6, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Earbuds', 'WIRE-0434', 'True wireless earbuds with charging case and touch controls', 67.32, TRUE, FALSE, NOW()
UNION
SELECT 'Golf Balls 12-Pack', 'GOLF-0435', 'Distance golf balls with durable cover', 18.83, TRUE, FALSE, NOW()
UNION
SELECT 'Noise Cancelling Earbuds', 'NOIS-0436', 'In-ear buds with active noise cancellation and 24-hour case battery', 83.97, TRUE, FALSE, NOW()
UNION
SELECT 'Unisex Baseball Cap', 'UNIS-0437', 'Adjustable cotton cap with curved brim', 11.02, TRUE, FALSE, NOW()
UNION
SELECT 'Athletic Ankle Socks', 'ATHL-0438', '6-pack moisture-wicking cushioned socks', 17.8, TRUE, FALSE, NOW()
UNION
SELECT '4K Webcam', 'KWEB-0439', '1080p/4K webcam with built-in microphone for streaming', 84.62, TRUE, FALSE, NOW()
UNION
SELECT 'Storage Bin Set', 'STOR-0440', 'Set of 6 stackable fabric storage bins with lids', 37.9, TRUE, FALSE, NOW()
UNION
SELECT 'Paper Shredder Basket', 'PAPE-0441', 'Compact home shredder for small offices', 38.27, TRUE, FALSE, NOW()
UNION
SELECT 'Robot Vacuum Cleaner', 'ROBO-0442', 'Smart robot vacuum with app control and auto-charging', 227.83, TRUE, FALSE, NOW()
UNION
SELECT 'Shower Curtain Set', 'SHOW-0443', 'Waterproof curtain with matching hooks', 17.18, TRUE, FALSE, NOW()
UNION
SELECT 'Phone Tripod Stand', 'PHON-0444', 'Flexible tripod with phone clip and remote shutter', 18.62, TRUE, FALSE, NOW()
UNION
SELECT 'Laundry Hamper Collapsible', 'LAUN-0445', 'Foldable fabric hamper with carry handles', 21.95, TRUE, FALSE, NOW()
UNION
SELECT 'Remote Control Car', 'REMO-0446', '4WD off-road RC car with rechargeable battery', 41.6, TRUE, FALSE, NOW()
UNION
SELECT 'Facial Cleansing Brush', 'FACI-0447', 'Silicone facial brush with waterproof design', 28.35, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Presenter Remote', 'WIRE-0448', 'PowerPoint clicker with laser pointer', 24.16, TRUE, FALSE, NOW()
UNION
SELECT 'Board Game Family Night', 'BOAR-0449', 'Strategy board game for 2-6 players, ages 8+', 21.81, TRUE, FALSE, NOW()
UNION
SELECT 'Smart Plug WiFi', 'SMAR-0450', 'App-controlled smart plug compatible with voice assistants', 10.92, TRUE, FALSE, NOW()
UNION
SELECT 'Standing Desk Converter', 'STAN-0451', 'Height-adjustable desktop riser for dual monitors', 117.87, TRUE, FALSE, NOW()
UNION
SELECT 'Area Rug 5x7', 'AREA-0452', 'Soft pile area rug for living room', 71.24, TRUE, FALSE, NOW()
UNION
SELECT 'Card Game Party Pack', 'CARD-0453', 'Fast-paced card game for family game nights', 13.9, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Pad Large', 'DESK-0454', 'Non-slip PU leather desk mat, 31x15 inches', 18.16, TRUE, FALSE, NOW()
UNION
SELECT 'Action Camera 4K', 'ACTI-0455', 'Waterproof action camera with mounting accessories kit', 87.75, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Bluetooth Headphones', 'WIRE-0456', 'Over-ear headphones with active noise cancellation and 30-hour battery life', 110.97, TRUE, FALSE, NOW()
UNION
SELECT 'Scented Soy Candle', 'SCEN-0457', 'Hand-poured candle with 40-hour burn time', 16.51, TRUE, FALSE, NOW()
UNION
SELECT 'Drone with Camera', 'DRON-0458', 'Beginner-friendly drone with HD camera and app control', 94.59, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Denim Jacket', 'WOME-0459', 'Classic cropped denim jacket with button closure', 43.52, TRUE, FALSE, NOW()
UNION
SELECT 'Hair Straightener Ceramic', 'HAIR-0460', 'Dual voltage flat iron with adjustable heat', 33.59, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Slim Fit Jeans', 'MENS-0461', 'Stretch denim jeans in dark wash', 38.91, TRUE, FALSE, NOW()
UNION
SELECT 'Wooden Train Set', 'WOOD-0462', 'Classic wooden track set with 40 pieces', 28.17, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Camping Stove', 'PORT-0463', 'Compact propane stove for outdoor cooking', 42.22, TRUE, FALSE, NOW()
UNION
SELECT 'Trekking Poles Pair', 'TREK-0464', 'Collapsible aluminum trekking poles with cork grips', 31.74, TRUE, FALSE, NOW()
UNION
SELECT 'Desk Organizer Set', 'DESK-0465', '5-piece mesh desk organizer for office supplies', 22.51, TRUE, FALSE, NOW()
UNION
SELECT 'Stainless Steel Water Bottle', 'STAI-0466', 'Insulated bottle keeps drinks cold for 24 hours', 23.39, TRUE, FALSE, NOW()
UNION
SELECT 'Adjustable Dumbbell Set', 'ADJU-0467', '5-25lb adjustable dumbbells, pair', 106.63, TRUE, FALSE, NOW()
UNION
SELECT 'Puzzle 1000 Piece', 'PUZZ-0468', 'Landscape jigsaw puzzle for adults', 18.43, TRUE, FALSE, NOW()
UNION
SELECT 'Jump Rope Speed', 'JUMP-0469', 'Adjustable steel cable jump rope with ball bearings', 11.9, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Ankle Boots', 'WOME-0470', 'Faux leather boots with side zipper', 47.17, TRUE, FALSE, NOW()
UNION
SELECT 'Ceramic Coffee Mug Set', 'CERA-0471', 'Set of 4 12oz mugs with matching saucers', 21.39, TRUE, FALSE, NOW()
UNION
SELECT 'Electric Kettle 1.7L', 'ELEC-0472', 'Rapid boil kettle with auto shut-off feature', 35.69, TRUE, FALSE, NOW()
UNION
SELECT 'Art Supplies Kit', 'ARTS-0473', '50-piece kids art set with case', 17.33, TRUE, FALSE, NOW()
UNION
SELECT 'Mechanical Gaming Keyboard', 'MECH-0474', 'RGB backlit keyboard with hot-swappable switches', 83.5, TRUE, FALSE, NOW()
UNION
SELECT 'Wireless Mouse', 'WIRE-0475', 'Ergonomic wireless mouse with adjustable DPI settings', 28.39, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Running Shoes', 'WOME-0476', 'Lightweight breathable sneakers with cushioned sole', 47.81, TRUE, FALSE, NOW()
UNION
SELECT 'Foam Roller', 'FOAM-0477', 'High-density muscle recovery foam roller, 18 inches', 18.5, TRUE, FALSE, NOW()
UNION
SELECT 'USB Hub 7-Port', 'USBH-0478', 'Powered USB 3.0 hub with individual switches', 21.82, TRUE, FALSE, NOW()
UNION
SELECT 'Men''s Leather Belt', 'MENS-0479', 'Genuine leather belt with metal buckle', 16.52, TRUE, FALSE, NOW()
UNION
SELECT 'Plush Stuffed Animal', 'PLUS-0480', 'Soft plush toy, 14 inches, machine washable', 13.7, TRUE, FALSE, NOW()
UNION
SELECT 'Makeup Brush Set', 'MAKE-0481', '12-piece professional makeup brush set with case', 19.14, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Block Set', 'YOGA-0482', '2-pack high-density foam yoga blocks', 12.31, TRUE, FALSE, NOW()
UNION
SELECT 'External SSD 1TB', 'EXTE-0483', 'Portable solid state drive with USB-C connector', 98.63, TRUE, FALSE, NOW()
UNION
SELECT 'Whiteboard Magnetic 24x36', 'WHIT-0484', 'Dry erase board with aluminum frame and marker tray', 31.24, TRUE, FALSE, NOW()
UNION
SELECT 'Portable Monitor 15.6-inch', 'PORT-0485', 'Full HD USB-C portable monitor with built-in speakers', 188.29, TRUE, FALSE, NOW()
UNION
SELECT 'Massage Gun Percussion', 'MASS-0486', 'Deep tissue massager with 6 attachment heads', 65.57, TRUE, FALSE, NOW()
UNION
SELECT 'Cotton Bed Sheet Set', 'COTT-0487', 'Queen size 4-piece sheet set, 100% cotton', 54.23, TRUE, FALSE, NOW()
UNION
SELECT 'Yoga Mat Non-Slip', 'YOGA-0488', '6mm thick eco-friendly yoga mat with carrying strap', 29.45, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Speaker', 'BLUE-0489', 'Waterproof portable speaker with 12-hour playtime', 30.58, TRUE, FALSE, NOW()
UNION
SELECT 'Bamboo Cutting Board Set', 'BAMB-0490', 'Set of 3 eco-friendly cutting boards', 28.8, TRUE, FALSE, NOW()
UNION
SELECT 'HDMI Cable 6ft', 'HDMI-0491', 'High-speed HDMI 2.1 cable supporting 8K resolution', 10.44, TRUE, FALSE, NOW()
UNION
SELECT 'Smartwatch Fitness Tracker', 'SMAR-0492', 'Heart rate monitor and step counter with 7-day battery', 73.84, TRUE, FALSE, NOW()
UNION
SELECT 'Office Chair Ergonomic', 'OFFI-0493', 'Mesh back chair with lumbar support and adjustable arms', 214.84, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Yoga Leggings', 'WOME-0494', 'High-waisted leggings with moisture-wicking fabric', 24.47, TRUE, FALSE, NOW()
UNION
SELECT 'Women''s Activewear Sports Bra', 'WOME-0495', 'Medium support bra with removable padding', 18.89, TRUE, FALSE, NOW()
UNION
SELECT 'Insulated Cooler Backpack', 'INSU-0496', '20L leak-proof cooler backpack for outdoor trips', 52.82, TRUE, FALSE, NOW()
UNION
SELECT 'Air Fryer 5.8QT', 'AIRF-0497', 'Digital air fryer with 8 preset cooking modes', 85.06, TRUE, FALSE, NOW()
UNION
SELECT 'Bluetooth Car Adapter', 'BLUE-0498', 'FM transmitter with hands-free calling and USB charging', 18.37, TRUE, FALSE, NOW()
UNION
SELECT 'Bicycle Helmet', 'BICY-0499', 'Adjustable ventilated helmet with LED safety light', 39.68, TRUE, FALSE, NOW()
UNION
SELECT 'Smartphone Gimbal Stabilizer', 'SMAR-0500', '3-axis gimbal for smooth video recording', 117.98, TRUE, FALSE, NOW();