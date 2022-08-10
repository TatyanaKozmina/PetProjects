using SchoolJournal.Data;
using SchoolJournal.Data.Repos;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Tests
{
    [TestClass]
    public class SchoolJournalDataJournalRecordTests
    {
        private BaseTest baseTest;
        SchoolJournalDBContext? dBContext;
        IPupilRepository pupilRepository;
        IStreamRepository streamRepository;
        IJournalRecordRepository journalRecordRepository;
        public SchoolJournalDataJournalRecordTests()
        {
            baseTest = new BaseTest();
            dBContext = new SchoolJournalDBContext(baseTest.dbContextOptions);
            pupilRepository = new PupilRepository(dBContext);
            streamRepository = new StreamRepository(dBContext);
            journalRecordRepository = new JournalRecordRepository(dBContext);
        }

        [TestMethod]
        public async Task JournalRepo_Create_WrongPupil()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();            

            //Analyze
            await Assert.ThrowsExceptionAsync<Exception>(() => journalRecordRepository.Create(Guid.NewGuid(), new JournalRecord { Mark = 4, Subject = Subject.Math, Created = DateTime.Today }));
        }

        [TestMethod]
        public async Task JournalRepo_Create()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();            
            await pupilRepository.Create(new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru", StreamId = stream.Id });
            Pupil pupil = dBContext.Pupils.FirstOrDefault();

            //Run method
            await journalRecordRepository.Create(pupil.Id, 
                                                 new JournalRecord { Mark = 4, Subject = Subject.Math, Created = DateTime.Today});

            //Analyze
            Assert.IsTrue(dBContext.JournalRecords.Count() == 1, "Journal record was not created");
        }
    }
}