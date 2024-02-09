USE datSampleDataBase
BEGIN TRAN
--Veicoli A900 (Auto e Moto)
INSERT	INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, BackGroundColor, BackGroundColorCssClass, IsActive) 
		SELECT	1, 'V01', 'Veicoli', '#FF0000', 'vehicle-insurance-coverage-btn', 1

INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 1, 'A01', 'Auto', '"Le polizze auto di Dat Assicurazioni offrono coperture complete e personalizzabili per ogni guidatore. Tariffe competitive e servizio clienti affidabile garantiti.', 'fa-solid fa-car', 1, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 2, 'B01', 'Moto', 'Le polizze per moto e scooter di Dat Assicurazioni offrono una protezione completa e su misura per ogni motociclista. Con coperture per responsabilità civile, danni da collisione, furto e incendio, assicuriamo la tua sicurezza su strada. Tariffe competitive e un servizio clienti affidabile completano il nostro impegno per la tua tranquillità.', 'fa-solid fa-motorcycle', 1, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 3, 'C01', 'Imbarcazione', 'Le polizze imbarcazioni di Dat Assicurazioni offrono coperture complete per proteggere la tua barca in mare. Includono danni accidentali, responsabilità civile, furto e incendio, garantendo sicurezza e tranquillità durante le tue avventure acquatiche', 'fa-solid fa-ship', 1, 0

--Viaggi (1007, 2007, 2107, 4807, H139, R304, W150)
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, BackGroundColor, BackGroundColorCssClass, IsActive) 
		SELECT	2, 'V02', 'Viaggi', '#CC0066', 'travel-insurance-coverage-btn', 1

INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 4, 'A02', 'Viaggio in Aereo', 'Le polizze per viaggi in aereo di Dat Assicurazioni offrono una copertura completa per assicurare la tua tranquillità durante i viaggi in volo. Coperture includono protezione per cancellazioni, ritardi, bagagli smarriti e assistenza medica d''emergenza, garantendo una sicurezza totale in ogni avventura aerea.', 'fa-solid fa-plane', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 5, 'B02', 'Viaggio in Treno', 'Le polizze per viaggi in treno di Dat Assicurazioni offrono una copertura completa per assicurare la tua tranquillità durante gli spostamenti su rotaia. Includono protezione per ritardi, cancellazioni, perdita o danneggiamento dei bagagli e assistenza medica d''emergenza, garantendo una sicurezza totale durante ogni viaggio in treno.', 'fa-solid fa-train', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 6, 'C02', 'Viaggio in Autobus', 'Le polizze per viaggi in autobus di Dat Assicurazioni offrono una copertura completa per assicurare la tua tranquillità durante gli spostamenti su strada. Includono protezione per ritardi, cancellazioni, assistenza medica d''emergenza e eventuali inconvenienti durante il viaggio, garantendo una sicurezza totale durante ogni viaggio in autobus.', 'fa-solid fa-bus', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 7, 'D02', 'Viaggio su Nave', 'Le polizze per viaggi in nave di Dat Assicurazioni garantiscono una copertura completa per assicurare la tua tranquillità durante le traversate marittime. Includono protezione per cancellazioni, ritardi, assistenza medica d''emergenza e smarrimento o danneggiamento dei bagagli, offrendo sicurezza totale durante ogni viaggio in nave.', 'fa-solid fa-sailboat', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 8, 'E02', 'Soggiorno in Casa Vacanze', 'Le polizze per soggiorni in casa vacanze di Dat Assicurazioni garantiscono una copertura completa per assicurare la tua tranquillità durante le tue vacanze. Includono protezione per eventuali danni alla proprietà, responsabilità civile, e assistenza in caso di emergenze durante il soggiorno, offrendo una vacanza senza preoccupazioni.', 'fa-solid fa-hotel', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 9, 'F02', 'Soggiorno in Hotel', 'Le polizze per soggiorni in hotel di Dat Assicurazioni offrono una copertura completa per garantire la tua tranquillità durante il soggiorno. Coprono eventuali danni alla proprietà, assistenza in caso di emergenze durante il soggiorno e eventuali problemi legati alla prenotazione, offrendo una vacanza senza preoccupazioni in hotel.', 'fa-solid fa-hotel', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 10, 'G02', 'Soggiorno in Villagio Turistico', 'Le polizze per soggiorni in villaggi turistici di Dat Assicurazioni garantiscono una copertura completa per assicurare la tua tranquillità durante le vacanze. Coprono danni alla proprietà, responsabilità civile, assistenza in caso di emergenze e problemi legati alla prenotazione, offrendo una vacanza senza preoccupazioni nel villaggio turistico.', 'fa-solid fa-hotel', 2, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 11, 'H02', 'Perdita Bagaglio', 'La polizza per la perdita del bagaglio di Dat Assicurazioni fornisce una copertura completa per proteggere i tuoi effetti personali durante i viaggi. In caso di smarrimento o danneggiamento del bagaglio, la polizza copre il costo di sostituzione degli oggetti persi o danneggiati, garantendo la tua tranquillità durante ogni viaggio.', 'fa-solid fa-suitcase-rolling', 2, 1


INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, BackGroundColor, BackGroundColorCssClass, IsActive) 
		SELECT	3, 'A03', 'Attivita'' Lavorativa', '#00FF80', 'work-insurance-coverage-btn', 1

--Attività Lavorativa ('R305', 'R306', 'R307', 'R308', 'R309', 'R310', 'R311', 'R312', 'R313', 'R314') Attività Professionale
--Attività Lavorativa ('I421', 'I480', 'I502', 'I503', 'NI01', 'U232', 'U233', 'U280', 'U281', 'U302', 'U303', 'U360', 'U499', 'U530') Locali e Fabbricati Aziendali
--Attività Agricole ('U750', 'U751', 'U752')
--Allevamento 'UB58'
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 12, 'A03', 'Attività Professionale', 'Le polizze per attività professionali di Dat Assicurazioni offrono una copertura completa per proteggere la tua attività dai rischi finanziari e legali. Con soluzioni su misura per ogni settore e professione, le nostre polizze coprono responsabilità civile, danni materiali, perdite finanziarie e altre eventualità impreviste, garantendo la sicurezza e la continuità del tuo business.', '', 3, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 13, 'B03', 'Immobile Aziendale', 'Le polizze per immobili aziendali di Dat Assicurazioni offrono una copertura completa per proteggere i tuoi beni immobili e le attività commerciali. Con opzioni che includono copertura per danni strutturali, furto, incendio, responsabilità civile e altri rischi specifici del settore immobiliare, garantiamo la sicurezza e la continuità delle tue operazioni aziendali.', 'fa-solid fa-industry', 3, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 14, 'C03', 'Attività Commerciale', 'Le polizze per attività commerciali di Dat Assicurazioni offrono una copertura completa per proteggere la tua azienda da una vasta gamma di rischi. Queste polizze possono includere coperture per danni alla proprietà, responsabilità civile, perdite finanziarie, interruzioni di attività e altro ancora. Personalizzabili per soddisfare le esigenze specifiche del tuo settore e della tua azienda, le nostre polizze assicurative assicurano la sicurezza e la stabilità del tuo business.', 'fa-solid fa-dumpster', 3, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 15, 'D03', 'Attività Agricola', 'La polizza per attività agricole di Dat Assicurazioni offre una copertura completa per proteggere la tua azienda agricola da una vasta gamma di rischi, inclusi danni alle colture, attrezzature, edifici e responsabilità civile. Personalizzabile per soddisfare le esigenze specifiche del settore agricolo, garantisce la sicurezza e la stabilità del tuo business.', 'fa-solid fa-tractor', 3, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 16, 'E03', 'Allevamento Bestiame', 'La polizza per l''allevamento di bestiame di Dat Assicurazioni protegge il tuo allevamento da danni, malattie e perdite dovute a eventi climatici avversi. Offrendo una copertura completa e personalizzabile, assicura la sicurezza e la stabilità del tuo business di allevamento di bestiame.', 'fa-solid fa-cow', 3, 1

--U164, U165, RI01     
INSERT	INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, BackGroundColor, BackGroundColorCssClass, IsActive) 
		SELECT	4, 'F04', 'Famiglia', '#FF00FF', 'family-insurance-coverage-btn', 1

INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 17, 'A04', 'Familiare e Congiunto', 'Le polizze per familiari e congiunti di Dat Assicurazioni offrono una copertura completa per proteggere i tuoi cari in caso di imprevisti. Queste polizze possono includere coperture per assistenza sanitaria, infortuni, invalidità, vita e altro ancora, garantendo la sicurezza e il benessere della tua famiglia. Con opzioni flessibili e vantaggi personalizzabili, ci impegniamo a fornire una protezione adeguata per le esigenze uniche dei tuoi cari.', 'fa-solid fa-people-group', 4, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 18, 'B04', 'Animale Domestico', 'Le polizze per animali domestici di Dat Assicurazioni offrono una copertura completa per proteggere il benessere del tuo fedele compagno. Coprendo spese veterinarie, interventi chirurgici, terapie e altro ancora, queste polizze assicurative garantiscono la salute e il comfort del tuo animale domestico. Con opzioni personalizzabili e assistenza dedicata, ci impegniamo a fornire la migliore cura possibile per i tuoi amici pelosi.', 'fa-solid fa-paw', 4, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 19, 'C04', 'Casa', 'Le polizze sulla propria abitazione di Dat Assicurazioni offrono una copertura completa per proteggere la tua casa e i tuoi beni. Coprendo danni strutturali, furto, incendio, eventi naturali e responsabilità civile, queste polizze assicurative garantiscono la sicurezza e la tranquillità della tua abitazione. Con opzioni personalizzabili e assistenza dedicata, ci impegniamo a fornire la protezione adeguata per le esigenze uniche della tua casa.', 'fa-solid fa-people-roof', 4, 1

--Infortunio (K025, K324, K328, K342, K380, K555, K556, K660, K661, K902, K901)
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, BackGroundColor, BackGroundColorCssClass, IsActive) 
		SELECT	5, 'S05', 'Salute', '#00FFFF', 'health-insurance-coverage-btn', 1

INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 20, 'A05', 'Infortunio', 'La polizza infortuni di Dat Assicurazioni fornisce una copertura completa per proteggerti finanziariamente in caso di infortuni personali. Copre le spese mediche, l''invalidità e fornisce un indennizzo in caso di morte accidentale. Con tariffe competitive e opzioni personalizzabili, garantiamo la tua sicurezza e quella dei tuoi cari in caso di imprevisti.', 'fa-solid fa-user-injured', 5, 1
--Malattia ('K329', 'K331', 'W027', 'W221', 'W222', 'W555', 'W556', 'W660')
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 21, 'B05', 'Malattia', 'La polizza malattia di Dat Assicurazioni offre una copertura completa per le spese mediche e le cure necessarie in caso di malattia. Garantisce assistenza finanziaria per visite mediche, trattamenti, ospedalizzazioni e altri costi correlati alla salute. Con opzioni flessibili e assistenza dedicata, ti assicuriamo tranquillità e benessere durante il percorso di guarigione.', 'fa-solid fa-syringe', 5, 1
--Visita Specialistica ('W315', 'W881', 'W380', 'W400', 'W830', 'W840')
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 22, 'C05', 'Visite Specialistiche', 'La polizza visite specialistiche di Dat Assicurazioni offre una copertura completa per le spese sostenute durante le visite mediche specialistiche. Copre costi come consulenze, esami diagnostici, e trattamenti specialistici. Con tariffe competitive e opzioni personalizzabili, garantiamo l''accesso a cure specialistiche senza preoccupazioni finanziarie.', 'fa-solid fa-stethoscope', 5, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 23, 'D05', 'Grandi Interventi', 'La polizza Grandi Interventi di Dat Assicurazioni offre una copertura completa per i costi elevati e imprevisti associati a interventi medici importanti come chirurgia o terapie intensive. Questa polizza assicurativa garantisce una protezione finanziaria adeguata per affrontare tali situazioni, permettendo di concentrarsi sulla propria salute senza preoccupazioni finanziarie.', 'fa-solid fa-hospital', 5, 1
INSERT	INTO AppInsurancePolicyCategories
		(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, InsurancePolicyCategoryDescription, IconCssClass, SalesLineId, IsActive) 
		SELECT 24, 'E05', 'Cure Odontoiatriche', 'La polizza Cure Odontoiatriche di Dat Assicurazioni fornisce una copertura completa per le spese odontoiatriche, inclusi trattamenti di base, cura dei denti, protesi, e interventi chirurgici. Con questa polizza, puoi garantire un sorriso sano e luminoso senza preoccuparti dei costi elevati associati alle cure dentali.', 'fa-solid fa-tooth', 5, 1
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 0, 'None', 'Non Specificato', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 1, 'Contratto_A_Termine', 'Contratto a Termine - (CDD)', 0.5, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 2, 'Contratto_Tempo_Indeterminato', 'Contratto a Tempo Determinato - (CDI)', 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 3, 'Contratto_Apprendistato', 'Contratto di Apprendistato', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 4, 'Partita_Iva', 'Partita IVA', 0.25, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 5, 'Contratto_CO_CO_PRO', 'Contratto a Progetto - (Co.Co.Pro.)', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 6, 'Contratto_CO_CO_CO', 'Contratto di collaborazione coordinata e continuativa - (Co.Co.Co.)', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 7, 'Contratto_Somministrazione', 'Contratto di somministrazione', 0.1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 8, 'Contratto_Lavoro_Intermittente', 'Contratto di lavoro Intermittente', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 9, 'Contratto_Lavoro_Part_Time', 'Contratto di lavoro Part Time', 0.1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 10, 'Contratto_Lavoro_Domicilio', 'Contratto di lavoro a Domicilio', 0, NULL, NULL, GETDATE(), GETDATE()

--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 0, 18000, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 18001, 28000, 0.05, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 28001, 38000, 0.1, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 38001, 48000, 0.15, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 5, 48001, 50000, 0.2, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 6, 50001, 55000, 0.25, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 7, 55001, 60000, 0.3, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 8, 60001, 65000, 0.35, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 9, 65001, 70000, 0.4, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 10, 70001, 75000, 0.45, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 11, 75001, 80000, 0.5, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 12, 80001, 85000, 0.6, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 13, 85001, 90000, 0.7, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 14, 90001, 100000, 0.8, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 15, 100001, NULL, 0.9, NULL, NULL, GETDATE(), GETDATE()

--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 0, 17, 0.5, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 18, 26, 0.15, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 27, 35, 0.25, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 36, 44, 0.5, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 5, 45, 53, 0.75, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 6, 54, 62, 0.75, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 7, 63, 71, 0.25, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 8, 72, 80, 0.15, 0, NULL, NULL, GETDATE(), GETDATE()
--INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 9, 81, NULL, 0, 0, NULL, NULL, GETDATE(), GETDATE()

ROLLBACK TRAN