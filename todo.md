# To-Do List

## 1. Create the MainController Class
- Implement the `MainController` class that orchestrates the entire process.
- Link all required processors and services.
- **Diagram File**: [MainController Class Diagram](./diagrams/MainController_Class_Diagram.md)

## 2. Implement the DirectoryReader
- Create a class that reads directories and applies filters.
- **Diagram File**: [DirectoryReader Class Diagram](./diagrams/DirectoryReader_Class_Diagram.md)

## 3. Create the OCR Process (including Anti-Corruption Layer)
- Develop a class that creates OCR requests and processes responses.
- **Diagram File**: [OCRProcess Class Diagram](./diagrams/OCRProcess_Class_Diagram.md)

## 4. Develop the LLM Process (including Anti-Corruption Layer)
- Develop a class that creates LLM requests and processes responses.
- **Diagram File**: [LLMProcess Class Diagram](./diagrams/LLMProcess_Class_Diagram.md)

## 5. Create the DB Interaction (including Anti-Corruption Layer)
- Develop a class that sends requests to the database and processes responses.
- **Diagram File**: [DBProcess Class Diagram](./diagrams/DBProcess_Class_Diagram.md)

## 6. Develop the FileMover
- Create a class that moves files based on the LLM results.
- **Diagram File**: [FileMover Class Diagram](./diagrams/FileMover_Class_Diagram.md)

## 7. Implement Error Logging
- Implement an error logging mechanism that logs all errors that occur.
- **Diagram File**: [ErrorLogger Class Diagram](./diagrams/ErrorLogger_Class_Diagram.md)

## 8. Create Service Classes
- Develop `OCRService`, `LLMService`, and `DBService`, each using their respective processors.
- **Diagram File**: [Service Classes Diagram](./diagrams/Service_Classes_Diagram.md)

## 9. Design DTOs (Data Transfer Objects)
- Create DTOs for OCR, LLM, and DB that will serve as the interface for data transfer.
- **Diagram File**: [DTOs Diagram](./diagrams/DTOs_Diagram.md)

## 10. Integration and Testing
- Combine all the implemented components and test the complete system.
- **Diagram File**: [Complete System Architecture Diagram](./diagrams/Complete_System_Architecture.md)
