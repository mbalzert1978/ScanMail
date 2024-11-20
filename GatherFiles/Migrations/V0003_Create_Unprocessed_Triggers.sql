CREATE TRIGGER tr_Unprocessed_Insert_CreatedAt
AFTER INSERT ON Unprocessed
FOR EACH ROW
WHEN NEW.CreatedAt IS NULL
BEGIN
    UPDATE Unprocessed SET CreatedAt = CURRENT_TIMESTAMP WHERE Id = NEW.Id;
END;

CREATE TRIGGER tr_Unprocessed_Update_ProcessedAt
AFTER UPDATE OF IsRead ON Unprocessed
FOR EACH ROW
WHEN NEW.IsRead = 1 AND OLD.IsRead = 0 AND NEW.ProcessedAt IS NULL
BEGIN
    UPDATE Unprocessed SET ProcessedAt = CURRENT_TIMESTAMP WHERE Id = NEW.Id;
END;

