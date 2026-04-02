Цел на оваа лабораториска вежба е да се имплементира целиот податочен тек:

Controller -> Mapper -> Service -> Repository -> DB и назад

Задачи:

Да се креира DTO ConsultationDto кое се користи во CreateAsync и UpdateAsync методите во ConsultationService
Да се имплементираат сите методи во ConsultationService
GetByIdNotNullAsync(Guid id): Task<Consultation> - враќа еден ентитет, никогаш null — потребен е соодветен exception handling
GetAllAsync(string? roomName): Task<List<Consultation>> - враќа листа ентитети; ако roomName не е null, применува филтер
CreateAsync(ConsultationDto dto): Task<Consultation> - прима DTO и го враќа зачуваниот ентитет
UpdateAsync(Guid id, ConsultationDto dto): Task<Consultation> - прима DTO и го враќа ажурираниот ентитет
DeleteAsync(Guid id): Task<Consultation> - го брише ентитетот и го враќа избришаниот
GetPagedAsync(int pageNumber, int pageSize): Task<PaginatedResult<Consultation>> - враќа одредена страница од базата 
Овој метод треба да враќа податоци и за сите Attendance објекти кои се наоѓаат во колекцијата
Не смее да се користи Lazy Load
Да се креира ConsultationMapper кој ги пресретнува барањата од Web слојот, ги повикува сервисните методи и ги мапира податоците во двете насоки.
Маперот треба да содржи методи за повикување на СИТЕ сервисни методи.
Да се креира record ConsultationResponse кој ќе има податоци за идентификаторот на консултациите, времето за почеток и крај на консултациите, идентификаторот за собата во која се одржуваат консултациите како и нејзиното име.
Да се креира record AttendanceResponse кој ќе има податоци за идентификаторот на присуството, идентификаторот на корисникот, целосното име на корисникот и статусот на истиот.
Притоа, потребно е да се прикаже текстуална информација за статусот, не вредноста на енумерацијата.
Да се креира record ConsultationWithAttendancesResponse кој ќе ги содржи сите податоци како и ConsultationResponse, но дополнително ќе чува и List<AttendanceResponse>.
Да се креира екстензија AttendanceMappingExtensions која ќе мапира:
Attendance -> AttendanceResponse
List<Attendance> -> List<AttendanceResponse>
Да се креира екстензија ConsultationMappingExtenstions која ќе ги мапира:
Consultation -> ConsultationResponse
Consultation -> ConsultationWithAttendancesResponse
List<Consultation> -> List<ConsultationResponse>
CreateOrUpdateConsultationRequest -> ConsultationDto
PaginatedResult<Consultation> -> PaginatedResponse<ConsultationWithAttendancesResponse>
Да се креира ConsultationsController на патека /api/consultations со следните endpoints:
GET /{id} - враќа една консултација според ID
GET /?dateAfter- ги враќа сите консултации; поддржува опционален query параметар dateAfter за филтрирање
GET /paged?pageNumber=&pageSize= - враќа пагинирани резултати; задолжителни параметри: pageNumber, pageSize (PaginatedRequest)
POST / - креира нов Consultation од телото на барањето
Само најавени корисници може да креираат консултации
PUT /{id} - ажурира постоечки Consultation со податоците од телото на барањето
Само најавени корисници може да ажурираат консултации
DELETE /{id} - брише постоечки Consultation според ID
