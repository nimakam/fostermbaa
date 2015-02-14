using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardReaderLibrary;
using Moq;
using System.Windows.Forms;

namespace CardReader.UnitTests
{
    [TestClass]
    public class StudentIdCaptureTests
    {
        [TestMethod]
        public void StudentIdCapture_recognizes_nima_student_id()
        {
            var studentId = "1326290";
            var studentIdSequence = @";10013262906610?
";
            var studentIdCapture = new StudentIdCapture();
            var studentIdSubscriberMock = new Mock<ISubscriber<string>>();
            studentIdCapture.Subscribe(studentIdSubscriberMock.Object);

            foreach (var character in studentIdSequence)
            {
                var keys = CardReaderLibrary.KeysConverter.ConvertToKeys(character);

                if (keys.HasValue)
                {
                    studentIdCapture.Play(keys.Value);
                }
            }

            studentIdSubscriberMock.Verify(subscriber => subscriber.Play(studentId));
        }
        [TestMethod]
        public void StudentIdCapture_recognizes_reza_student_id()
        {
            var studentId = "1367901";
            var studentIdSequence = @";10013679016510?
";
            var studentIdCapture = new StudentIdCapture();
            var studentIdSubscriberMock = new Mock<ISubscriber<string>>();
            studentIdCapture.Subscribe(studentIdSubscriberMock.Object);

            foreach (var character in studentIdSequence)
            {
                var keys = CardReaderLibrary.KeysConverter.ConvertToKeys(character);

                if (keys.HasValue)
                {
                    studentIdCapture.Play(keys.Value);
                }
            }

            studentIdSubscriberMock.Verify(subscriber => subscriber.Play(studentId));
        }

        [TestMethod]
        public void StudentIdCapture_does_not_recogniz_incorrect_student_id()
        {
            var studentIdSequence = @"%B5154629723037569^YOU/A GIFT FOR^21065211000000706000000?;5154629723037569=210652110000706?
";
            var studentIdCapture = new StudentIdCapture();
            var studentIdSubscriberMock = new Mock<ISubscriber<string>>();
            studentIdSubscriberMock.Setup(subscriber => subscriber.Play(It.IsAny<string>())).Throws(new Exception("Expected pay not to be invoked."));
            studentIdCapture.Subscribe(studentIdSubscriberMock.Object);

            foreach (var character in studentIdSequence)
            {
                var keys = CardReaderLibrary.KeysConverter.ConvertToKeys(character);

                if (keys.HasValue)
                {
                    studentIdCapture.Play(keys.Value);
                }
            }
        }
    }
}
