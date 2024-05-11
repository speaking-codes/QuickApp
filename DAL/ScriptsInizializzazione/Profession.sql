use datSampleDataBase
INSERT INTO AppIncomeTypes (Id, IncomeTypeDescription, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 'Reddito da Lavoro Dipendente', NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppIncomeTypes (Id, IncomeTypeDescription, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 'Reddito da Lavoro come Libero Professionista', NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppIncomeTypes (Id, IncomeTypeDescription, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 'Nessun Reddito', NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppIncomeTypes (Id, IncomeTypeDescription, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 'Altro Reddito', NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 'Operaio', 0, 18000, 25000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 'Impiegato', 0, 20000, 35000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 'Disoccupato', 0, 0, 0, 3, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 'Libero professionista', 1, 25000, 100000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 5, 'Commerciante', 1, 25000, 80000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 6, 'Agente di commercio', 1, 22000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 7, 'Altra professione', 1, 20000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 8, 'Insegnante', 0, 25000, 50000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 9, 'Pensionato', 0, 0, 0, 3, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 10, 'Cuoco', 0, 19000, 30000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 11, 'Casalinga', 0, 0, 0, 3, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 12, 'Artigiano', 1, 22000, 40000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 13, 'Studente', 0, 0, 0, 3, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 14, 'Avvocato', 1, 30000, 150000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 15, 'Appartenente a Forze Armate', 0, 22000, 60000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 16, 'Architetto', 1, 30000, 80000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 17, 'Consulente Commerciale', 1, 25000, 70000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 18, 'Medico', 1, 40000, 200000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 19, 'Agricoltore', 1, 18000, 40000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 20, 'Geometra', 1, 25000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 21, 'Imprenditore', 1, 30000, 150000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 22, 'Lavoratore autonomo', 1, 22000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 23, 'Cameriere', 1, 18000, 25000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 24, 'Infermiere', 0, 22000, 40000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 25, 'Ingegnere', 1, 30000, 80000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 26, 'Bracciante', 1, 18000, 25000, 4, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 27, 'Autista', 0, 19000, 30000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 28, 'Tecnico', 0, 22000, 50000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 29, 'Dipendente generico', 0, 20000, 40000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 30, 'Fotografo', 1, 20000, 50000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 31, 'Dipendente Ente privato', 0, 20000, 40000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 32, 'Farmacista', 0, 25000, 60000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 33, 'Giornalista', 1, 22000, 50000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 34, 'Rappresentante', 1, 22000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 35, 'Dipendente Ente pubblico', 0, 22000, 50000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 36, 'Manovale', 0, 18000, 25000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 37, 'Dirigente', 0, 40000, 200000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 38, 'Marittimo', 0, 18000, 30000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 39, 'Magistrato', 0, 40000, 200000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 40, 'Quadro', 0, 30000, 150000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 41, 'Agronomo', 1, 25000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 42, 'Disegnatore', 1, 20000, 40000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 43, 'Artista', 1, 18000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 44, 'Dimostratore scientifico', 1, 25000, 70000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 45, 'Sportivo', 1, 25000, 100000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 46, 'Chimico', 0, 25000, 80000, 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 47, 'Religioso', 0, 0, 0, 4, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 48, 'Societa''', 1, 80000, 500000, 4, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 49, 'Interprete', 1, 22000, 60000, 2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppProfessionTypes(Id, ProfessionTypeDescription, IsFreelancer, MinAnnualGrossIncome, MaxAnnualGrossIncome, IncomeTypeId, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 50, 'Consulente Turistico', 1, 25000, 70000, 2, NULL, NULL, GETDATE(), GETDATE()