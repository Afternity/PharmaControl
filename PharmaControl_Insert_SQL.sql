-- Очистка таблиц (если нужно)
DELETE FROM Sales;
DELETE FROM PharmacyStocks;
DELETE FROM Medicines;
DELETE FROM MedicineTypes;
DELETE FROM Employees;
DELETE FROM Pharmacies;
GO

-- 1. Добавляем типы лекарств (10 записей) - было 5 (+5)
INSERT INTO MedicineTypes (Id, Name, Description)
VALUES
    (NEWID(), 'Антибиотики', 'Препараты для лечения бактериальных инфекций'),
    (NEWID(), 'Обезболивающие', 'Препараты для снятия боли различного происхождения'),
    (NEWID(), 'Противовирусные', 'Препараты для лечения вирусных заболеваний'),
    (NEWID(), 'Витамины', 'Биологически активные добавки и витаминные комплексы'),
    (NEWID(), 'Антигистаминные', 'Препараты против аллергических реакций'),
    (NEWID(), 'Кардиологические', 'Препараты для лечения сердечно-сосудистых заболеваний'),
    (NEWID(), 'Гастроэнтерологические', 'Препараты для лечения ЖКТ'),
    (NEWID(), 'Неврологические', 'Препараты для лечения нервной системы'),
    (NEWID(), 'Эндокринологические', 'Препараты для лечения гормональных нарушений'),
    (NEWID(), 'Дерматологические', 'Препараты для лечения кожных заболеваний');
GO

-- 2. Добавляем аптеки (8 записей) - было 3 (+5)
DECLARE @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER, @Pharmacy4 UNIQUEIDENTIFIER, 
        @Pharmacy5 UNIQUEIDENTIFIER, @Pharmacy6 UNIQUEIDENTIFIER, @Pharmacy7 UNIQUEIDENTIFIER, @Pharmacy8 UNIQUEIDENTIFIER;

INSERT INTO Pharmacies (Id, Name, Address, PhoneNumber, Email, OpeningTime, ClosingTime)
VALUES
    (NEWID(), 'Аптека №1 Центральная', 'ул. Центральная, д. 10', '+7-495-111-11-11', 'apteka1@pharma.ru', '08:00:00', '22:00:00'),
    (NEWID(), 'Аптека №2 Ленина', 'ул. Ленина, д. 25', '+7-495-222-22-22', 'apteka2@pharma.ru', '09:00:00', '21:00:00'),
    (NEWID(), 'Аптека №3 Пушкина', 'ул. Пушкина, д. 15', '+7-495-333-33-33', 'apteka3@pharma.ru', '08:30:00', '23:00:00'),
    (NEWID(), 'Аптека №4 Садовая', 'ул. Садовая, д. 7', '+7-495-444-44-44', 'apteka4@pharma.ru', '10:00:00', '20:00:00'),
    (NEWID(), 'Аптека №5 Мира', 'пр. Мира, д. 100', '+7-495-555-55-55', 'apteka5@pharma.ru', '08:00:00', '00:00:00'),
    (NEWID(), 'Аптека №6 Северная', 'ул. Северная, д. 50', '+7-495-666-66-66', 'apteka6@pharma.ru', '09:00:00', '22:00:00'),
    (NEWID(), 'Аптека №7 Южная', 'ул. Южная, д. 30', '+7-495-777-77-77', 'apteka7@pharma.ru', '08:00:00', '23:30:00'),
    (NEWID(), 'Аптека №8 Западная', 'ул. Западная, д. 15', '+7-495-888-88-88', 'apteka8@pharma.ru', '07:30:00', '22:00:00');

-- Получаем ID добавленных аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1 Центральная';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2 Ленина';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3 Пушкина';
SELECT @Pharmacy4 = Id FROM Pharmacies WHERE Name = 'Аптека №4 Садовая';
SELECT @Pharmacy5 = Id FROM Pharmacies WHERE Name = 'Аптека №5 Мира';
SELECT @Pharmacy6 = Id FROM Pharmacies WHERE Name = 'Аптека №6 Северная';
SELECT @Pharmacy7 = Id FROM Pharmacies WHERE Name = 'Аптека №7 Южная';
SELECT @Pharmacy8 = Id FROM Pharmacies WHERE Name = 'Аптека №8 Западная';
GO

-- 3. Добавляем сотрудников (15 записей) - было 5 (+10)
DECLARE @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER, 
        @Pharmacy4 UNIQUEIDENTIFIER, @Pharmacy5 UNIQUEIDENTIFIER, @Pharmacy6 UNIQUEIDENTIFIER, 
        @Pharmacy7 UNIQUEIDENTIFIER, @Pharmacy8 UNIQUEIDENTIFIER;

SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1 Центральная';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2 Ленина';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3 Пушкина';
SELECT @Pharmacy4 = Id FROM Pharmacies WHERE Name = 'Аптека №4 Садовая';
SELECT @Pharmacy5 = Id FROM Pharmacies WHERE Name = 'Аптека №5 Мира';
SELECT @Pharmacy6 = Id FROM Pharmacies WHERE Name = 'Аптека №6 Северная';
SELECT @Pharmacy7 = Id FROM Pharmacies WHERE Name = 'Аптека №7 Южная';
SELECT @Pharmacy8 = Id FROM Pharmacies WHERE Name = 'Аптека №8 Западная';

INSERT INTO Employees (Id, FullName, Email, Password, Phone, PharmacyId)
VALUES
    -- Аптека №1 (3 сотрудника)
    (NEWID(), 'Иванова Анна Сергеевна', 'ivanova@pharma.ru', 'pharm123', '+7-911-111-11-11', @Pharmacy1),
    (NEWID(), 'Петров Михаил Иванович', 'petrov@pharma.ru', 'pharm456', '+7-911-222-22-22', @Pharmacy1),
    (NEWID(), 'Смирнова Ольга Викторовна', 'smirnova@pharma.ru', 'pharm789', '+7-911-333-33-33', @Pharmacy1),
    
    -- Аптека №2 (2 сотрудника)
    (NEWID(), 'Сидорова Елена Викторовна', 'sidorova@pharma.ru', 'pharm012', '+7-911-444-44-44', @Pharmacy2),
    (NEWID(), 'Кузнецов Андрей Петрович', 'kuznetsov@pharma.ru', 'pharm345', '+7-911-555-55-55', @Pharmacy2),
    
    -- Аптека №3 (2 сотрудника)
    (NEWID(), 'Морозова Ольга Дмитриевна', 'morozova@pharma.ru', 'pharm678', '+7-911-666-66-66', @Pharmacy3),
    (NEWID(), 'Васильев Дмитрий Сергеевич', 'vasiliev@pharma.ru', 'pharm901', '+7-911-777-77-77', @Pharmacy3),
    
    -- Аптека №4 (2 сотрудника)
    (NEWID(), 'Николаева Мария Петровна', 'nikolaeva@pharma.ru', 'pharm234', '+7-911-888-88-88', @Pharmacy4),
    (NEWID(), 'Семенов Игорь Владимирович', 'semenov@pharma.ru', 'pharm567', '+7-911-999-99-99', @Pharmacy4),
    
    -- Аптека №5 (2 сотрудника)
    (NEWID(), 'Федорова Татьяна Андреевна', 'fedorova@pharma.ru', 'pharm890', '+7-911-000-00-00', @Pharmacy5),
    (NEWID(), 'Павлов Алексей Николаевич', 'pavlov@pharma.ru', 'pharm111', '+7-911-123-45-67', @Pharmacy5),
    
    -- Аптека №6 (2 сотрудника)
    (NEWID(), 'Козлова Екатерина Игоревна', 'kozlova@pharma.ru', 'pharm222', '+7-911-234-56-78', @Pharmacy6),
    (NEWID(), 'Лебедев Сергей Васильевич', 'lebedev@pharma.ru', 'pharm333', '+7-911-345-67-89', @Pharmacy6),
    
    -- Аптека №7 (1 сотрудник)
    (NEWID(), 'Новикова Анна Михайловна', 'novikova@pharma.ru', 'pharm444', '+7-911-456-78-90', @Pharmacy7),
    
    -- Аптека №8 (1 сотрудник)
    (NEWID(), 'Медведев Павел Олегович', 'medvedev@pharma.ru', 'pharm555', '+7-911-567-89-01', @Pharmacy8);
GO

-- 4. Добавляем лекарства (25 записей) - было 10 (+15)
DECLARE @AntibioticsId UNIQUEIDENTIFIER, @PainkillersId UNIQUEIDENTIFIER, @AntiviralId UNIQUEIDENTIFIER, 
        @VitaminsId UNIQUEIDENTIFIER, @AntihistaminesId UNIQUEIDENTIFIER, @CardioId UNIQUEIDENTIFIER, 
        @GastroId UNIQUEIDENTIFIER, @NeuroId UNIQUEIDENTIFIER, @EndoId UNIQUEIDENTIFIER, @DermaId UNIQUEIDENTIFIER;

SELECT @AntibioticsId = Id FROM MedicineTypes WHERE Name = 'Антибиотики';
SELECT @PainkillersId = Id FROM MedicineTypes WHERE Name = 'Обезболивающие';
SELECT @AntiviralId = Id FROM MedicineTypes WHERE Name = 'Противовирусные';
SELECT @VitaminsId = Id FROM MedicineTypes WHERE Name = 'Витамины';
SELECT @AntihistaminesId = Id FROM MedicineTypes WHERE Name = 'Антигистаминные';
SELECT @CardioId = Id FROM MedicineTypes WHERE Name = 'Кардиологические';
SELECT @GastroId = Id FROM MedicineTypes WHERE Name = 'Гастроэнтерологические';
SELECT @NeuroId = Id FROM MedicineTypes WHERE Name = 'Неврологические';
SELECT @EndoId = Id FROM MedicineTypes WHERE Name = 'Эндокринологические';
SELECT @DermaId = Id FROM MedicineTypes WHERE Name = 'Дерматологические';

INSERT INTO Medicines (Id, Name, Description, Price, RequiresPrescription, BestBeforeDate, MedicineTypeId)
VALUES
    -- Антибиотики (5 шт)
    (NEWID(), 'Амоксициллин', 'Антибиотик широкого спектра действия', 450.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntibioticsId),
    (NEWID(), 'Азитромицин', 'Макролидный антибиотик', 650.00, 1, DATEADD(YEAR, 3, GETDATE()), @AntibioticsId),
    (NEWID(), 'Цефтриаксон', 'Цефалоспориновый антибиотик', 1200.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntibioticsId),
    (NEWID(), 'Доксициклин', 'Тетрациклиновый антибиотик', 550.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntibioticsId),
    (NEWID(), 'Ципрофлоксацин', 'Фторхинолоновый антибиотик', 480.00, 1, DATEADD(YEAR, 3, GETDATE()), @AntibioticsId),
    
    -- Обезболивающие (5 шт)
    (NEWID(), 'Ибупрофен', 'Нестероидный противовоспалительный препарат', 250.00, 0, DATEADD(YEAR, 2, GETDATE()), @PainkillersId),
    (NEWID(), 'Парацетамол', 'Жаропонижающее и обезболивающее средство', 150.00, 0, DATEADD(YEAR, 3, GETDATE()), @PainkillersId),
    (NEWID(), 'Кетанов', 'Сильное обезболивающее', 850.00, 1, DATEADD(YEAR, 2, GETDATE()), @PainkillersId),
    (NEWID(), 'Найз', 'Противовоспалительное средство', 320.00, 0, DATEADD(YEAR, 2, GETDATE()), @PainkillersId),
    (NEWID(), 'Спазмалгон', 'Спазмолитик и анальгетик', 280.00, 0, DATEADD(YEAR, 3, GETDATE()), @PainkillersId),
    
    -- Противовирусные (3 шт)
    (NEWID(), 'Арбидол', 'Противовирусный препарат', 950.00, 0, DATEADD(YEAR, 2, GETDATE()), @AntiviralId),
    (NEWID(), 'Осельтамивир', 'Противогриппозное средство', 1200.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntiviralId),
    (NEWID(), 'Ингавирин', 'Противовирусное средство', 1100.00, 0, DATEADD(YEAR, 2, GETDATE()), @AntiviralId),
    
    -- Витамины (3 шт)
    (NEWID(), 'Компливит', 'Витаминно-минеральный комплекс', 750.00, 0, DATEADD(YEAR, 1, GETDATE()), @VitaminsId),
    (NEWID(), 'Витрум', 'Поливитамины', 1200.00, 0, DATEADD(YEAR, 2, GETDATE()), @VitaminsId),
    (NEWID(), 'Супрадин', 'Витаминный комплекс', 850.00, 0, DATEADD(YEAR, 1, GETDATE()), @VitaminsId),
    
    -- Антигистаминные (2 шт)
    (NEWID(), 'Лоратадин', 'Против аллергии', 350.00, 0, DATEADD(YEAR, 2, GETDATE()), @AntihistaminesId),
    (NEWID(), 'Цетрин', 'Антигистаминный препарат', 420.00, 0, DATEADD(YEAR, 3, GETDATE()), @AntihistaminesId),
    
    -- Кардиологические (2 шт)
    (NEWID(), 'Анаприлин', 'Бета-адреноблокатор', 180.00, 1, DATEADD(YEAR, 2, GETDATE()), @CardioId),
    (NEWID(), 'Каптоприл', 'Ингибитор АПФ', 220.00, 1, DATEADD(YEAR, 3, GETDATE()), @CardioId),
    
    -- Гастроэнтерологические (2 шт)
    (NEWID(), 'Омепразол', 'Ингибитор протонной помпы', 310.00, 1, DATEADD(YEAR, 2, GETDATE()), @GastroId),
    (NEWID(), 'Фосфалюгель', 'Антацидное средство', 280.00, 0, DATEADD(YEAR, 2, GETDATE()), @GastroId),
    
    -- Неврологические (1 шт)
    (NEWID(), 'Глицин', 'Ноотропное средство', 120.00, 0, DATEADD(YEAR, 3, GETDATE()), @NeuroId),
    
    -- Эндокринологические (1 шт)
    (NEWID(), 'Метформин', 'Противодиабетическое средство', 290.00, 1, DATEADD(YEAR, 2, GETDATE()), @EndoId),
    
    -- Дерматологические (1 шт)
    (NEWID(), 'Акридерм', 'Кортикостероидная мазь', 340.00, 1, DATEADD(YEAR, 1, GETDATE()), @DermaId);
GO

-- 5. Добавляем запасы в аптеках (складские позиции) - 50 записей вместо 15
DECLARE 
    @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER,
    @Pharmacy4 UNIQUEIDENTIFIER, @Pharmacy5 UNIQUEIDENTIFIER, @Pharmacy6 UNIQUEIDENTIFIER,
    @Pharmacy7 UNIQUEIDENTIFIER, @Pharmacy8 UNIQUEIDENTIFIER,
    @AmoxicillinId UNIQUEIDENTIFIER, @AzithromycinId UNIQUEIDENTIFIER, @CeftriaxoneId UNIQUEIDENTIFIER,
    @DoxycyclineId UNIQUEIDENTIFIER, @CiprofloxacinId UNIQUEIDENTIFIER, @IbuprofenId UNIQUEIDENTIFIER, 
    @ParacetamolId UNIQUEIDENTIFIER, @KetanovId UNIQUEIDENTIFIER, @NiseId UNIQUEIDENTIFIER,
    @SpazmalgonId UNIQUEIDENTIFIER, @ArbidolId UNIQUEIDENTIFIER, @OseltamivirId UNIQUEIDENTIFIER,
    @IngavirinId UNIQUEIDENTIFIER, @ComplivitId UNIQUEIDENTIFIER, @VitrumId UNIQUEIDENTIFIER,
    @SupradinId UNIQUEIDENTIFIER, @LoratadineId UNIQUEIDENTIFIER, @CetrinId UNIQUEIDENTIFIER,
    @AnaprilinId UNIQUEIDENTIFIER, @CaptoprilId UNIQUEIDENTIFIER, @OmeprazoleId UNIQUEIDENTIFIER,
    @PhosphalugelId UNIQUEIDENTIFIER, @GlycineId UNIQUEIDENTIFIER, @MetforminId UNIQUEIDENTIFIER,
    @AkriderId UNIQUEIDENTIFIER;

-- Получаем ID аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1 Центральная';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2 Ленина';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3 Пушкина';
SELECT @Pharmacy4 = Id FROM Pharmacies WHERE Name = 'Аптека №4 Садовая';
SELECT @Pharmacy5 = Id FROM Pharmacies WHERE Name = 'Аптека №5 Мира';
SELECT @Pharmacy6 = Id FROM Pharmacies WHERE Name = 'Аптека №6 Северная';
SELECT @Pharmacy7 = Id FROM Pharmacies WHERE Name = 'Аптека №7 Южная';
SELECT @Pharmacy8 = Id FROM Pharmacies WHERE Name = 'Аптека №8 Западная';

-- Получаем ID лекарств
SELECT @AmoxicillinId = Id FROM Medicines WHERE Name = 'Амоксициллин';
SELECT @AzithromycinId = Id FROM Medicines WHERE Name = 'Азитромицин';
SELECT @CeftriaxoneId = Id FROM Medicines WHERE Name = 'Цефтриаксон';
SELECT @DoxycyclineId = Id FROM Medicines WHERE Name = 'Доксициклин';
SELECT @CiprofloxacinId = Id FROM Medicines WHERE Name = 'Ципрофлоксацин';
SELECT @IbuprofenId = Id FROM Medicines WHERE Name = 'Ибупрофен';
SELECT @ParacetamolId = Id FROM Medicines WHERE Name = 'Парацетамол';
SELECT @KetanovId = Id FROM Medicines WHERE Name = 'Кетанов';
SELECT @NiseId = Id FROM Medicines WHERE Name = 'Найз';
SELECT @SpazmalgonId = Id FROM Medicines WHERE Name = 'Спазмалгон';
SELECT @ArbidolId = Id FROM Medicines WHERE Name = 'Арбидол';
SELECT @OseltamivirId = Id FROM Medicines WHERE Name = 'Осельтамивир';
SELECT @IngavirinId = Id FROM Medicines WHERE Name = 'Ингавирин';
SELECT @ComplivitId = Id FROM Medicines WHERE Name = 'Компливит';
SELECT @VitrumId = Id FROM Medicines WHERE Name = 'Витрум';
SELECT @SupradinId = Id FROM Medicines WHERE Name = 'Супрадин';
SELECT @LoratadineId = Id FROM Medicines WHERE Name = 'Лоратадин';
SELECT @CetrinId = Id FROM Medicines WHERE Name = 'Цетрин';
SELECT @AnaprilinId = Id FROM Medicines WHERE Name = 'Анаприлин';
SELECT @CaptoprilId = Id FROM Medicines WHERE Name = 'Каптоприл';
SELECT @OmeprazoleId = Id FROM Medicines WHERE Name = 'Омепразол';
SELECT @PhosphalugelId = Id FROM Medicines WHERE Name = 'Фосфалюгель';
SELECT @GlycineId = Id FROM Medicines WHERE Name = 'Глицин';
SELECT @MetforminId = Id FROM Medicines WHERE Name = 'Метформин';
SELECT @AkriderId = Id FROM Medicines WHERE Name = 'Акридерм';

-- Запасы для Аптеки №1 (8 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy1, @AmoxicillinId, 50, 10, 100),
    (@Pharmacy1, @IbuprofenId, 100, 20, 150),
    (@Pharmacy1, @ParacetamolId, 150, 30, 200),
    (@Pharmacy1, @ComplivitId, 40, 5, 80),
    (@Pharmacy1, @LoratadineId, 60, 10, 100),
    (@Pharmacy1, @OmeprazoleId, 30, 5, 60),
    (@Pharmacy1, @GlycineId, 80, 15, 120),
    (@Pharmacy1, @PhosphalugelId, 45, 10, 80);

-- Запасы для Аптеки №2 (7 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy2, @AzithromycinId, 30, 5, 60),
    (@Pharmacy2, @KetanovId, 25, 5, 50),
    (@Pharmacy2, @ArbidolId, 45, 10, 80),
    (@Pharmacy2, @CetrinId, 55, 10, 100),
    (@Pharmacy2, @AnaprilinId, 40, 8, 70),
    (@Pharmacy2, @VitrumId, 25, 5, 50),
    (@Pharmacy2, @IngavirinId, 35, 7, 60);

-- Запасы для Аптеки №3 (6 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy3, @OseltamivirId, 20, 3, 40),
    (@Pharmacy3, @IbuprofenId, 80, 15, 120),
    (@Pharmacy3, @ParacetamolId, 120, 25, 180),
    (@Pharmacy3, @LoratadineId, 40, 8, 70),
    (@Pharmacy3, @CaptoprilId, 35, 7, 60),
    (@Pharmacy3, @SupradinId, 30, 6, 50);

-- Запасы для Аптеки №4 (7 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy4, @CeftriaxoneId, 15, 3, 30),
    (@Pharmacy4, @NiseId, 60, 12, 100),
    (@Pharmacy4, @SpazmalgonId, 50, 10, 80),
    (@Pharmacy4, @MetforminId, 40, 8, 70),
    (@Pharmacy4, @ComplivitId, 35, 7, 60),
    (@Pharmacy4, @ArbidolId, 30, 6, 50),
    (@Pharmacy4, @GlycineId, 70, 14, 110);

-- Запасы для Аптеки №5 (6 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy5, @DoxycyclineId, 25, 5, 50),
    (@Pharmacy5, @KetanovId, 20, 4, 40),
    (@Pharmacy5, @OseltamivirId, 18, 4, 35),
    (@Pharmacy5, @VitrumId, 30, 6, 50),
    (@Pharmacy5, @CetrinId, 45, 9, 80),
    (@Pharmacy5, @AnaprilinId, 35, 7, 60);

-- Запасы для Аптеки №6 (6 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy6, @CiprofloxacinId, 30, 6, 50),
    (@Pharmacy6, @ParacetamolId, 100, 20, 150),
    (@Pharmacy6, @NiseId, 55, 11, 90),
    (@Pharmacy6, @IngavirinId, 25, 5, 45),
    (@Pharmacy6, @SupradinId, 40, 8, 70),
    (@Pharmacy6, @OmeprazoleId, 35, 7, 60);

-- Запасы для Аптеки №7 (5 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy7, @AmoxicillinId, 40, 8, 70),
    (@Pharmacy7, @IbuprofenId, 70, 14, 110),
    (@Pharmacy7, @SpazmalgonId, 45, 9, 80),
    (@Pharmacy7, @ComplivitId, 30, 6, 50),
    (@Pharmacy7, @LoratadineId, 50, 10, 85);

-- Запасы для Аптеки №8 (5 позиций)
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy8, @AzithromycinId, 25, 5, 45),
    (@Pharmacy8, @ArbidolId, 40, 8, 70),
    (@Pharmacy8, @PhosphalugelId, 60, 12, 100),
    (@Pharmacy8, @CetrinId, 35, 7, 60),
    (@Pharmacy8, @AkriderId, 20, 4, 40);
GO

-- 6. Добавляем продажи (100 записей) - было 15 (+85)
DECLARE 
    @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER,
    @Pharmacy4 UNIQUEIDENTIFIER, @Pharmacy5 UNIQUEIDENTIFIER, @Pharmacy6 UNIQUEIDENTIFIER,
    @Pharmacy7 UNIQUEIDENTIFIER, @Pharmacy8 UNIQUEIDENTIFIER,
    @Employee1 UNIQUEIDENTIFIER, @Employee2 UNIQUEIDENTIFIER, @Employee3 UNIQUEIDENTIFIER,
    @Employee4 UNIQUEIDENTIFIER, @Employee5 UNIQUEIDENTIFIER, @Employee6 UNIQUEIDENTIFIER,
    @Employee7 UNIQUEIDENTIFIER, @Employee8 UNIQUEIDENTIFIER, @Employee9 UNIQUEIDENTIFIER,
    @Employee10 UNIQUEIDENTIFIER, @Employee11 UNIQUEIDENTIFIER, @Employee12 UNIQUEIDENTIFIER,
    @Employee13 UNIQUEIDENTIFIER, @Employee14 UNIQUEIDENTIFIER, @Employee15 UNIQUEIDENTIFIER,
    @AmoxicillinId UNIQUEIDENTIFIER, @AzithromycinId UNIQUEIDENTIFIER, @CeftriaxoneId UNIQUEIDENTIFIER,
    @DoxycyclineId UNIQUEIDENTIFIER, @CiprofloxacinId UNIQUEIDENTIFIER, @IbuprofenId UNIQUEIDENTIFIER, 
    @ParacetamolId UNIQUEIDENTIFIER, @KetanovId UNIQUEIDENTIFIER, @NiseId UNIQUEIDENTIFIER,
    @SpazmalgonId UNIQUEIDENTIFIER, @ArbidolId UNIQUEIDENTIFIER, @OseltamivirId UNIQUEIDENTIFIER,
    @IngavirinId UNIQUEIDENTIFIER, @ComplivitId UNIQUEIDENTIFIER, @VitrumId UNIQUEIDENTIFIER,
    @SupradinId UNIQUEIDENTIFIER, @LoratadineId UNIQUEIDENTIFIER, @CetrinId UNIQUEIDENTIFIER,
    @AnaprilinId UNIQUEIDENTIFIER, @CaptoprilId UNIQUEIDENTIFIER, @OmeprazoleId UNIQUEIDENTIFIER,
    @PhosphalugelId UNIQUEIDENTIFIER, @GlycineId UNIQUEIDENTIFIER, @MetforminId UNIQUEIDENTIFIER,
    @AkriderId UNIQUEIDENTIFIER;

-- Получаем ID аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1 Центральная';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2 Ленина';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3 Пушкина';
SELECT @Pharmacy4 = Id FROM Pharmacies WHERE Name = 'Аптека №4 Садовая';
SELECT @Pharmacy5 = Id FROM Pharmacies WHERE Name = 'Аптека №5 Мира';
SELECT @Pharmacy6 = Id FROM Pharmacies WHERE Name = 'Аптека №6 Северная';
SELECT @Pharmacy7 = Id FROM Pharmacies WHERE Name = 'Аптека №7 Южная';
SELECT @Pharmacy8 = Id FROM Pharmacies WHERE Name = 'Аптека №8 Западная';

-- Получаем ID сотрудников
SELECT TOP 1 @Employee1 = Id FROM Employees WHERE Email = 'ivanova@pharma.ru';
SELECT TOP 1 @Employee2 = Id FROM Employees WHERE Email = 'petrov@pharma.ru';
SELECT TOP 1 @Employee3 = Id FROM Employees WHERE Email = 'smirnova@pharma.ru';
SELECT TOP 1 @Employee4 = Id FROM Employees WHERE Email = 'sidorova@pharma.ru';
SELECT TOP 1 @Employee5 = Id FROM Employees WHERE Email = 'kuznetsov@pharma.ru';
SELECT TOP 1 @Employee6 = Id FROM Employees WHERE Email = 'morozova@pharma.ru';
SELECT TOP 1 @Employee7 = Id FROM Employees WHERE Email = 'vasiliev@pharma.ru';
SELECT TOP 1 @Employee8 = Id FROM Employees WHERE Email = 'nikolaeva@pharma.ru';
SELECT TOP 1 @Employee9 = Id FROM Employees WHERE Email = 'semenov@pharma.ru';
SELECT TOP 1 @Employee10 = Id FROM Employees WHERE Email = 'fedorova@pharma.ru';
SELECT TOP 1 @Employee11 = Id FROM Employees WHERE Email = 'pavlov@pharma.ru';
SELECT TOP 1 @Employee12 = Id FROM Employees WHERE Email = 'kozlova@pharma.ru';
SELECT TOP 1 @Employee13 = Id FROM Employees WHERE Email = 'lebedev@pharma.ru';
SELECT TOP 1 @Employee14 = Id FROM Employees WHERE Email = 'novikova@pharma.ru';
SELECT TOP 1 @Employee15 = Id FROM Employees WHERE Email = 'medvedev@pharma.ru';

-- Получаем ID лекарств
SELECT @AmoxicillinId = Id FROM Medicines WHERE Name = 'Амоксициллин';
SELECT @AzithromycinId = Id FROM Medicines WHERE Name = 'Азитромицин';
SELECT @CeftriaxoneId = Id FROM Medicines WHERE Name = 'Цефтриаксон';
SELECT @DoxycyclineId = Id FROM Medicines WHERE Name = 'Доксициклин';
SELECT @CiprofloxacinId = Id FROM Medicines WHERE Name = 'Ципрофлоксацин';
SELECT @IbuprofenId = Id FROM Medicines WHERE Name = 'Ибупрофен';
SELECT @ParacetamolId = Id FROM Medicines WHERE Name = 'Парацетамол';
SELECT @KetanovId = Id FROM Medicines WHERE Name = 'Кетанов';
SELECT @NiseId = Id FROM Medicines WHERE Name = 'Найз';
SELECT @SpazmalgonId = Id FROM Medicines WHERE Name = 'Спазмалгон';
SELECT @ArbidolId = Id FROM Medicines WHERE Name = 'Арбидол';
SELECT @OseltamivirId = Id FROM Medicines WHERE Name = 'Осельтамивир';
SELECT @IngavirinId = Id FROM Medicines WHERE Name = 'Ингавирин';
SELECT @ComplivitId = Id FROM Medicines WHERE Name = 'Компливит';
SELECT @VitrumId = Id FROM Medicines WHERE Name = 'Витрум';
SELECT @SupradinId = Id FROM Medicines WHERE Name = 'Супрадин';
SELECT @LoratadineId = Id FROM Medicines WHERE Name = 'Лоратадин';
SELECT @CetrinId = Id FROM Medicines WHERE Name = 'Цетрин';
SELECT @AnaprilinId = Id FROM Medicines WHERE Name = 'Анаприлин';
SELECT @CaptoprilId = Id FROM Medicines WHERE Name = 'Каптоприл';
SELECT @OmeprazoleId = Id FROM Medicines WHERE Name = 'Омепразол';
SELECT @PhosphalugelId = Id FROM Medicines WHERE Name = 'Фосфалюгель';
SELECT @GlycineId = Id FROM Medicines WHERE Name = 'Глицин';
SELECT @MetforminId = Id FROM Medicines WHERE Name = 'Метформин';
SELECT @AkriderId = Id FROM Medicines WHERE Name = 'Акридерм';

INSERT INTO Sales (Id, SaleDate, Amount, PaymentMethod, PharmacyId, MedicineId)
VALUES
    -- Продажи в Аптеке №1 (20 продаж)
    (NEWID(), GETDATE(), 2, 1, @Pharmacy1, @AmoxicillinId),
    (NEWID(), GETDATE(), 1, 0, @Pharmacy1, @IbuprofenId),
    (NEWID(), GETDATE(), 3, 1, @Pharmacy1, @ParacetamolId),
    (NEWID(), DATEADD(DAY, -1, GETDATE()), 2, 0, @Pharmacy1, @ComplivitId),
    (NEWID(), DATEADD(DAY, -1, GETDATE()), 