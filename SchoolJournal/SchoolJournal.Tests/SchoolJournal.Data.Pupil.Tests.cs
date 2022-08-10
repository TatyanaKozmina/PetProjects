using SchoolJournal.Data;
using SchoolJournal.Data.Repos;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Tests
{
    [TestClass]
    public class SchoolJournalDataPupilTests
    {
        private BaseTest baseTest;
        SchoolJournalDBContext? dBContext;
        IPupilRepository pupilRepository;
        IStreamRepository streamRepository;
        public SchoolJournalDataPupilTests()
        {
            baseTest = new BaseTest();
            dBContext = new SchoolJournalDBContext(baseTest.dbContextOptions);
            pupilRepository = new PupilRepository(dBContext);
            streamRepository = new StreamRepository(dBContext);
        }

        [TestMethod]
        public async Task PupilRepo_Create_EmptyStream()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            //await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            //var stream = dBContext.Streams.FirstOrDefault();

            //Run method
            //await pupilRepository.Create(new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru"});


            //Analyze
            await Assert.ThrowsExceptionAsync<Exception>(() => pupilRepository.Create(new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru" }));

            //await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => pupilRepository.Test());
        }

        [TestMethod]
        public async Task PupilRepo_Create()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();

            //Run method
            await pupilRepository.Create(new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru", StreamId = stream.Id});

            //Analyze
            Assert.IsTrue(dBContext.Pupils.Count() == 1, "Pupil was not created");
        }

        [TestMethod]
        public async Task PupilRepo_Delete()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();
            Pupil pupil = new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru", StreamId = stream.Id };
            await pupilRepository.Create(pupil);
            Assert.IsTrue(dBContext.Pupils.Count() == 1, "Pupil was not created");

            //Run method
            await pupilRepository.Delete(pupil.Id);

            //Analyze
            Assert.IsTrue(dBContext.Pupils.Count() == 0, "Pupil was not deleted");
        }

        [TestMethod]
        public async Task PupilRepo_GetById()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();
            Pupil pupil = new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru", StreamId = stream.Id };
            await pupilRepository.Create(pupil);
            Assert.IsTrue(dBContext.Pupils.Count() == 1, "Pupil was not created");

            //Run method
            var testPupil = await pupilRepository.GetById(pupil.Id);

            //Analyze
            Assert.AreEqual(testPupil, pupil, "Wrong pupil is returned");
        }

        [TestMethod]
        public async Task PupilRepo_GetAll()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();
            await pupilRepository.Create(new Pupil { FirstName = "Test1", LastName = "Test1", Email = "test1@mail.ru", StreamId = stream.Id });
            await pupilRepository.Create(new Pupil { FirstName = "Test2", LastName = "Test2", Email = "test2@mail.ru", StreamId = stream.Id });
            await pupilRepository.Create(new Pupil { FirstName = "Test3", LastName = "Test3", Email = "test3@mail.ru", StreamId = stream.Id });
            Assert.IsTrue(dBContext.Pupils.Count() == 3, "Pupils were not created");

            //Run method
            var pupils = await pupilRepository.GetAll(stream.Id);

            //Analyze
            Assert.AreEqual(pupils.Count, 3, "Wrong pupils number is returned");
        }

        [TestMethod]
        public async Task PupilRepo_Update()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();
            Pupil pupil = new Pupil { FirstName = "Test", LastName = "Test", Email = "test@mail.ru", StreamId = stream.Id };
            await pupilRepository.Create(pupil);
            Assert.IsTrue(dBContext.Pupils.Count() == 1, "Pupil was not created");

            //Run method
            string firstName = "NewFirstName";
            string lastName = "NewLastName";
            pupil.FirstName = firstName;
            pupil.LastName = lastName;
            await pupilRepository.Update(pupil);

            //Analyze
            Pupil updPupil = dBContext.Pupils.FirstOrDefault();
            Assert.IsTrue(updPupil.FirstName == firstName && updPupil.LastName == lastName,
                          "Pupil was not updated");
        }

        [TestMethod]
        public async Task PupilRepo_Search()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            var stream = dBContext.Streams.FirstOrDefault();
            await pupilRepository.Create(new Pupil { FirstName = "Anna", LastName = "Ivanova", Email = "test1@mail.ru", StreamId = stream.Id });
            await pupilRepository.Create(new Pupil { FirstName = "Max", LastName = "Titov", Email = "test2@mail.ru", StreamId = stream.Id });
            Assert.IsTrue(dBContext.Pupils.Count() == 2, "Pupils was not created");

            // Test empty result
            var pupils = await pupilRepository.Search("qwerty");
            Assert.IsFalse(pupils.Any(), "Result should be empty");

            // Test search by first name
            pupils = await pupilRepository.Search("ma");
            Assert.IsTrue(pupils.Count() == 1, "Only 1 pupil should be in list");
            Assert.IsTrue(pupils[0].LastName == "Titov", "Wrong pupil is returned");

            // Test search by last name
            pupils = await pupilRepository.Search("van");
            Assert.IsTrue(pupils.Count() == 1, "Only 1 pupil should be in list");
            Assert.IsTrue(pupils[0].FirstName == "Anna", "Wrong pupil is returned");
        }
    }
}