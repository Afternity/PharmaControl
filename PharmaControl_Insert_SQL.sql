-- Очистка таблиц (если нужно)
DELETE FROM Sales;
DELETE FROM PharmacyStocks;
DELETE FROM Medicines;
DELETE FROM MedicineTypes;
DELETE FROM Employees;
DELETE FROM Pharmacies;
GO

-- 1. Добавляем типы лекарств (5 записей)
INSERT INTO MedicineTypes (Id, Name, Description)
VALUES
    (NEWID(), 'Антибиотики', 'Препараты для лечения бактериальных инфекций'),
    (NEWID(), 'Обезболивающие', 'Препараты для снятия боли различного происхождения'),
    (NEWID(), 'Противовирусные', 'Препараты для лечения вирусных заболеваний'),
    (NEWID(), 'Витамины', 'Биологически активные добавки и витаминные комплексы'),
    (NEWID(), 'Антигистаминные', 'Препараты против аллергических реакций');
GO

-- 2. Добавляем аптеки (3 записи) и сразу получаем их ID
DECLARE @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER;

INSERT INTO Pharmacies (Id, Name, Address, PhoneNumber, Email, OpeningTime, ClosingTime)
VALUES
    (NEWID(), 'Аптека №1', 'ул. Центральная, д. 10', '+7-495-111-11-11', 'apteka1@pharma.ru', '08:00:00', '22:00:00'),
    (NEWID(), 'Аптека №2', 'ул. Ленина, д. 25', '+7-495-222-22-22', 'apteka2@pharma.ru', '09:00:00', '21:00:00'),
    (NEWID(), 'Аптека №3', 'ул. Пушкина, д. 15', '+7-495-333-33-33', 'apteka3@pharma.ru', '08:30:00', '23:00:00');

-- Получаем ID добавленных аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3';
GO

-- 3. Добавляем сотрудников (5 записей)
DECLARE @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER;

SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3';

INSERT INTO Employees (Id, FullName, Email, Password, Phone, PharmacyId)
VALUES
    (NEWID(), 'Иванова Анна Сергеевна', 'ivanova@pharma.ru', 'pharm123', '+7-911-111-11-11', @Pharmacy1),
    (NEWID(), 'Петров Михаил Иванович', 'petrov@pharma.ru', 'pharm456', '+7-911-222-22-22', @Pharmacy1),
    (NEWID(), 'Сидорова Елена Викторовна', 'sidorova@pharma.ru', 'pharm789', '+7-911-333-33-33', @Pharmacy2),
    (NEWID(), 'Кузнецов Андрей Петрович', 'kuznetsov@pharma.ru', 'pharm012', '+7-911-444-44-44', @Pharmacy2),
    (NEWID(), 'Морозова Ольга Дмитриевна', 'morozova@pharma.ru', 'pharm345', '+7-911-555-55-55', @Pharmacy3);
GO

-- 4. Добавляем лекарства (10 записей)
DECLARE @AntibioticsId UNIQUEIDENTIFIER, @PainkillersId UNIQUEIDENTIFIER, @AntiviralId UNIQUEIDENTIFIER, @VitaminsId UNIQUEIDENTIFIER, @AntihistaminesId UNIQUEIDENTIFIER;

SELECT @AntibioticsId = Id FROM MedicineTypes WHERE Name = 'Антибиотики';
SELECT @PainkillersId = Id FROM MedicineTypes WHERE Name = 'Обезболивающие';
SELECT @AntiviralId = Id FROM MedicineTypes WHERE Name = 'Противовирусные';
SELECT @VitaminsId = Id FROM MedicineTypes WHERE Name = 'Витамины';
SELECT @AntihistaminesId = Id FROM MedicineTypes WHERE Name = 'Антигистаминные';

INSERT INTO Medicines (Id, Name, Description, Price, RequiresPrescription, BestBeforeDate, MedicineTypeId)
VALUES
    -- Антибиотики
    (NEWID(), 'Амоксициллин', 'Антибиотик широкого спектра действия', 450.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntibioticsId),
    (NEWID(), 'Азитромицин', 'Макролидный антибиотик', 650.00, 1, DATEADD(YEAR, 3, GETDATE()), @AntibioticsId),
    
    -- Обезболивающие
    (NEWID(), 'Ибупрофен', 'Нестероидный противовоспалительный препарат', 250.00, 0, DATEADD(YEAR, 2, GETDATE()), @PainkillersId),
    (NEWID(), 'Парацетамол', 'Жаропонижающее и обезболивающее средство', 150.00, 0, DATEADD(YEAR, 3, GETDATE()), @PainkillersId),
    (NEWID(), 'Кетанов', 'Сильное обезболивающее', 850.00, 1, DATEADD(YEAR, 2, GETDATE()), @PainkillersId),
    
    -- Противовирусные
    (NEWID(), 'Арбидол', 'Противовирусный препарат', 950.00, 0, DATEADD(YEAR, 2, GETDATE()), @AntiviralId),
    (NEWID(), 'Осельтамивир', 'Противогриппозное средство', 1200.00, 1, DATEADD(YEAR, 2, GETDATE()), @AntiviralId),
    
    -- Витамины
    (NEWID(), 'Компливит', 'Витаминно-минеральный комплекс', 750.00, 0, DATEADD(YEAR, 1, GETDATE()), @VitaminsId),
    
    -- Антигистаминные
    (NEWID(), 'Лоратадин', 'Против аллергии', 350.00, 0, DATEADD(YEAR, 2, GETDATE()), @AntihistaminesId),
    (NEWID(), 'Цетрин', 'Антигистаминный препарат', 420.00, 0, DATEADD(YEAR, 3, GETDATE()), @AntihistaminesId);
GO

-- 5. Добавляем запасы в аптеках (складские позиции)
DECLARE 
    @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER,
    @AmoxicillinId UNIQUEIDENTIFIER, @AzithromycinId UNIQUEIDENTIFIER, @IbuprofenId UNIQUEIDENTIFIER, 
    @ParacetamolId UNIQUEIDENTIFIER, @KetanovId UNIQUEIDENTIFIER, @ArbidolId UNIQUEIDENTIFIER,
    @OseltamivirId UNIQUEIDENTIFIER, @ComplivitId UNIQUEIDENTIFIER, @LoratadineId UNIQUEIDENTIFIER, @CetrinId UNIQUEIDENTIFIER;

-- Получаем ID аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3';

-- Получаем ID лекарств
SELECT @AmoxicillinId = Id FROM Medicines WHERE Name = 'Амоксициллин';
SELECT @AzithromycinId = Id FROM Medicines WHERE Name = 'Азитромицин';
SELECT @IbuprofenId = Id FROM Medicines WHERE Name = 'Ибупрофен';
SELECT @ParacetamolId = Id FROM Medicines WHERE Name = 'Парацетамол';
SELECT @KetanovId = Id FROM Medicines WHERE Name = 'Кетанов';
SELECT @ArbidolId = Id FROM Medicines WHERE Name = 'Арбидол';
SELECT @OseltamivirId = Id FROM Medicines WHERE Name = 'Осельтамивир';
SELECT @ComplivitId = Id FROM Medicines WHERE Name = 'Компливит';
SELECT @LoratadineId = Id FROM Medicines WHERE Name = 'Лоратадин';
SELECT @CetrinId = Id FROM Medicines WHERE Name = 'Цетрин';

-- Запасы для Аптеки №1
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy1, @AmoxicillinId, 50, 10, 100),
    (@Pharmacy1, @IbuprofenId, 100, 20, 150),
    (@Pharmacy1, @ParacetamolId, 150, 30, 200),
    (@Pharmacy1, @ComplivitId, 40, 5, 80),
    (@Pharmacy1, @LoratadineId, 60, 10, 100);

-- Запасы для Аптеки №2
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy2, @AzithromycinId, 30, 5, 60),
    (@Pharmacy2, @KetanovId, 25, 5, 50),
    (@Pharmacy2, @ArbidolId, 45, 10, 80),
    (@Pharmacy2, @CetrinId, 55, 10, 100);

-- Запасы для Аптеки №3
INSERT INTO PharmacyStocks (PharmacyId, MedicineId, Quantity, MinStockLevel, ReorderQuantity)
VALUES
    (@Pharmacy3, @OseltamivirId, 20, 3, 40),
    (@Pharmacy3, @IbuprofenId, 80, 15, 120),
    (@Pharmacy3, @ParacetamolId, 120, 25, 180),
    (@Pharmacy3, @LoratadineId, 40, 8, 70);
GO

-- 6. Добавляем продажи (15 записей)
DECLARE 
    @Pharmacy1 UNIQUEIDENTIFIER, @Pharmacy2 UNIQUEIDENTIFIER, @Pharmacy3 UNIQUEIDENTIFIER,
    @Employee1 UNIQUEIDENTIFIER, @Employee2 UNIQUEIDENTIFIER, @Employee3 UNIQUEIDENTIFIER,
    @Employee4 UNIQUEIDENTIFIER, @Employee5 UNIQUEIDENTIFIER,
    @AmoxicillinId UNIQUEIDENTIFIER, @AzithromycinId UNIQUEIDENTIFIER, @IbuprofenId UNIQUEIDENTIFIER, 
    @ParacetamolId UNIQUEIDENTIFIER, @KetanovId UNIQUEIDENTIFIER, @ArbidolId UNIQUEIDENTIFIER,
    @OseltamivirId UNIQUEIDENTIFIER, @ComplivitId UNIQUEIDENTIFIER, @LoratadineId UNIQUEIDENTIFIER, @CetrinId UNIQUEIDENTIFIER;

-- Получаем ID аптек
SELECT @Pharmacy1 = Id FROM Pharmacies WHERE Name = 'Аптека №1';
SELECT @Pharmacy2 = Id FROM Pharmacies WHERE Name = 'Аптека №2';
SELECT @Pharmacy3 = Id FROM Pharmacies WHERE Name = 'Аптека №3';

-- Получаем ID сотрудников
SELECT TOP 1 @Employee1 = Id FROM Employees WHERE Email = 'ivanova@pharma.ru';
SELECT TOP 1 @Employee2 = Id FROM Employees WHERE Email = 'petrov@pharma.ru';
SELECT TOP 1 @Employee3 = Id FROM Employees WHERE Email = 'sidorova@pharma.ru';
SELECT TOP 1 @Employee4 = Id FROM Employees WHERE Email = 'kuznetsov@pharma.ru';
SELECT TOP 1 @Employee5 = Id FROM Employees WHERE Email = 'morozova@pharma.ru';

-- Получаем ID лекарств
SELECT @AmoxicillinId = Id FROM Medicines WHERE Name = 'Амоксициллин';
SELECT @AzithromycinId = Id FROM Medicines WHERE Name = 'Азитромицин';
SELECT @IbuprofenId = Id FROM Medicines WHERE Name = 'Ибупрофен';
SELECT @ParacetamolId = Id FROM Medicines WHERE Name = 'Парацетамол';
SELECT @KetanovId = Id FROM Medicines WHERE Name = 'Кетанов';
SELECT @ArbidolId = Id FROM Medicines WHERE Name = 'Арбидол';
SELECT @OseltamivirId = Id FROM Medicines WHERE Name = 'Осельтамивир';
SELECT @ComplivitId = Id FROM Medicines WHERE Name = 'Компливит';
SELECT @LoratadineId = Id FROM Medicines WHERE Name = 'Лоратадин';
SELECT @CetrinId = Id FROM Medicines WHERE Name = 'Цетрин';

INSERT INTO Sales (Id, SaleDate, Amount, PaymentMethod, PharmacyId, MedicineId)
VALUES
    -- Продажи в Аптеке №1 (сегодняшние)
    (NEWID(), GETDATE(), 2, 1, @Pharmacy1, @AmoxicillinId),
    (NEWID(), GETDATE(), 1, 0, @Pharmacy1, @IbuprofenId),
    (NEWID(), GETDATE(), 3, 1, @Pharmacy1, @ParacetamolId),
    
    -- Продажи в Аптеке №2 (вчера)
    (NEWID(), DATEADD(DAY, -1, GETDATE()), 1, 0, @Pharmacy2, @AzithromycinId),
    (NEWID(), DATEADD(DAY, -1, GETDATE()), 2, 2, @Pharmacy2, @ArbidolId),
    (NEWID(), DATEADD(DAY, -1, GETDATE()), 1, 1, @Pharmacy2, @CetrinId),
    
    -- Продажи в Аптеке №3 (недельной давности)
    (NEWID(), DATEADD(DAY, -7, GETDATE()), 1, 0, @Pharmacy3, @OseltamivirId),
    (NEWID(), DATEADD(DAY, -6, GETDATE()), 5, 1, @Pharmacy3, @IbuprofenId),
    
    -- Старые продажи (месяц назад)
    (NEWID(), DATEADD(MONTH, -1, GETDATE()), 2, 0, @Pharmacy1, @ComplivitId),
    (NEWID(), DATEADD(MONTH, -1, GETDATE()), 1, 1, @Pharmacy2, @KetanovId),
    
    -- Продажи в этом месяце
    (NEWID(), DATEADD(DAY, -15, GETDATE()), 3, 0, @Pharmacy1, @LoratadineId),
    (NEWID(), DATEADD(DAY, -10, GETDATE()), 2, 2, @Pharmacy2, @AzithromycinId),
    (NEWID(), DATEADD(DAY, -5, GETDATE()), 4, 1, @Pharmacy3, @ParacetamolId),
    
    -- Крупные продажи
    (NEWID(), DATEADD(DAY, -3, GETDATE()), 10, 0, @Pharmacy1, @ParacetamolId),
    (NEWID(), DATEADD(DAY, -2, GETDATE()), 8, 1, @Pharmacy3, @LoratadineId);
GO

-- Простая проверка количества записей в таблицах
SELECT 'MedicineTypes' as TableName, COUNT(*) as Count FROM MedicineTypes
UNION ALL
SELECT 'Pharmacies', COUNT(*) FROM Pharmacies
UNION ALL
SELECT 'Employees', COUNT(*) FROM Employees
UNION ALL
SELECT 'Medicines', COUNT(*) FROM Medicines
UNION ALL
SELECT 'PharmacyStocks', COUNT(*) FROM PharmacyStocks
UNION ALL
SELECT 'Sales', COUNT(*) FROM Sales;
GO

-- Детальная проверка каждой таблицы
PRINT '=== ДЕТАЛЬНАЯ ПРОВЕРКА ФАРМАЦЕВТИЧЕСКОЙ БАЗЫ ДАННЫХ ===';

PRINT '1. Типы лекарств (MedicineTypes):';
SELECT Id, Name, Description FROM MedicineTypes;

PRINT '2. Аптеки (Pharmacies):';
SELECT Id, Name, Address, PhoneNumber, OpeningTime, ClosingTime FROM Pharmacies;

PRINT '3. Сотрудники (Employees):';
SELECT e.Id, e.FullName, e.Email, e.Phone, p.Name as PharmacyName 
FROM Employees e 
LEFT JOIN Pharmacies p ON e.PharmacyId = p.Id;

PRINT '4. Лекарства (Medicines):';
SELECT m.Id, m.Name, m.Price, m.RequiresPrescription, m.BestBeforeDate, mt.Name as MedicineType 
FROM Medicines m 
LEFT JOIN MedicineTypes mt ON m.MedicineTypeId = mt.Id;

PRINT '5. Складские запасы (PharmacyStocks):';
SELECT 
    p.Name as Pharmacy,
    m.Name as Medicine,
    ps.Quantity,
    ps.MinStockLevel,
    ps.ReorderQuantity,
    CASE 
        WHEN ps.Quantity <= ps.MinStockLevel THEN 'ТРЕБУЕТСЯ ЗАКАЗ'
        ELSE 'В НОРМЕ'
    END as StockStatus
FROM PharmacyStocks ps
LEFT JOIN Pharmacies p ON ps.PharmacyId = p.Id
LEFT JOIN Medicines m ON ps.MedicineId = m.Id
ORDER BY p.Name, m.Name;

PRINT '6. Продажи (Sales):';
SELECT 
    s.Id,
    p.Name as Pharmacy,
    m.Name as Medicine,
    s.SaleDate,
    s.Amount,
    CASE s.PaymentMethod
        WHEN 0 THEN 'Наличные'
        WHEN 1 THEN 'Карта'
        WHEN 2 THEN 'Страховка'
    END as PaymentMethod,
    (s.Amount * m.Price) as TotalPrice
FROM Sales s
LEFT JOIN Pharmacies p ON s.PharmacyId = p.Id
LEFT JOIN Medicines m ON s.MedicineId = m.Id
ORDER BY s.SaleDate DESC;
GO