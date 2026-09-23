using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Analaizer = AnalaizerClassLibrary.AnalaizerClass;

namespace AnalaizerClass.Tests
{
    [TestClass]
    public class CreateStackTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("System.Data.SqlClient",
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CalcTestsDB;Integrated Security=True",
            "CreateStackTestData",
            DataAccessMethod.Sequential)]
        public void CreateStack_DataDrivenTests()
        {
            //зчитує значення стовпців
            string inputExpr = TestContext.DataRow["Expression"].ToString();
            string expected = TestContext.DataRow["ExpectedOutput"].ToString();
            bool isError = Convert.ToBoolean(TestContext.DataRow["IsError"]);

            Analaizer.expression = inputExpr;

            string formatResult = Analaizer.Format();//Метод Format готує вираз: розставляє пробіли, обробляє оператори

            ArrayList actualStack = null;
            string actualError = null;

            try
            {
                actualStack = Analaizer.CreateStack();
            }
            catch (Exception ex)
            {
                actualError = ex.Message;
            }

            if (isError)
            {
                string resultString = formatResult.StartsWith("&")
                    ? formatResult
                    : (actualStack != null ? string.Join(" ", actualStack.ToArray()) : actualError);

                StringAssert.Contains(
                    resultString,
                    expected,
                    $"Вираз '{inputExpr}' мав завершитися помилкою {expected}");
            }
            else
            {
                Assert.IsNotNull(actualStack, $"Стек для '{inputExpr}' повернув null");
                string actualString = string.Join(" ", actualStack.ToArray());
                Assert.AreEqual(
                    expected,
                    actualString,
                    $"Некоректний ЗПЗ для виразу '{inputExpr}'");
            }
        }
    }
}