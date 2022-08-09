using SchoolJournal.Data;
using SchoolJournal.Data.Repos;

namespace SchoolJournal.Tests
{
    [TestClass]
    public class SchoolJournalDataStreamTests
    {
        private BaseTest baseTest;
        SchoolJournalDBContext? dBContext;
        IStreamRepository streamRepository;

        public SchoolJournalDataStreamTests()
        {
            baseTest = new BaseTest();
            dBContext = new SchoolJournalDBContext(baseTest.dbContextOptions);
            streamRepository = new StreamRepository(dBContext);
        }

        [TestMethod]
        public async Task StreamRepo_Create()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();

            //Run method
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });

            //Analyze
            Assert.IsTrue(dBContext.Streams.Count() == 1, "Stream was not created");
        }

        [TestMethod]
        public async Task StreamRepo_Delete()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            Models.DB.Stream stream = new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 };
            await streamRepository.Create(stream);
            Assert.IsTrue(dBContext.Streams.Count() == 1, "Stream was not created");

            //Run method
            await streamRepository.Delete(stream.Id);

            //Analyze
            Assert.IsTrue(dBContext.Streams.Count() == 0, "Stream was not deleted");
        }

        [TestMethod]
        public async Task StreamRepo_GetById()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            Models.DB.Stream stream = new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 };
            await streamRepository.Create(stream);
            Assert.IsTrue(dBContext.Streams.Count() == 1, "Stream was not created");

            //Run method
            var testStream = await streamRepository.GetById(stream.Id);

            //Analyze
            Assert.AreEqual(testStream, stream, "Wrong stream is returned");
        }

        [TestMethod]
        public async Task StreamRepo_GetAll()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 });
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today.AddDays(1), CurrentClass = 2 });
            await streamRepository.Create(new Models.DB.Stream { Started = DateTime.Today.AddDays(2), CurrentClass = 3 });
            Assert.IsTrue(dBContext.Streams.Count() == 3, "Streams were not created");

            //Run method
            var streams = await streamRepository.GetAll();

            //Analyze
            Assert.AreEqual(streams.Count, 3, "Wrong streams number is returned");
        }

        [TestMethod]
        public async Task StreamRepo_Update()
        {
            //Prerequisite
            await dBContext.Database.EnsureDeletedAsync();
            Models.DB.Stream stream = new Models.DB.Stream { Started = DateTime.Today, CurrentClass = 1 };
            await streamRepository.Create(stream);
            Assert.IsTrue(dBContext.Streams.Count() == 1, "Streams were not created");

            //Run method
            DateTime started = DateTime.Today.AddDays(1);
            int currClass = 2;
            bool completed = true;
            stream.Started = started;
            stream.CurrentClass = currClass;
            stream.IsCompleted = completed;
            await streamRepository.Update(stream);

            //Analyze
            Models.DB.Stream updStream = dBContext.Streams.FirstOrDefault();
            Assert.IsTrue(updStream.Started == started && updStream.CurrentClass == currClass && updStream.IsCompleted == completed, 
                          "Stream was not updated");
        }
    }
}