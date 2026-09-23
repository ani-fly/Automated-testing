using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using AnalaizerClassLibrary;

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
            // 1. Arrange: вичитуємо дані з поточного рядка БД
            string inputExpr = TestContext.DataRow["Expression"].ToString();
            string expected = TestContext.DataRow["ExpectedOutput"].ToString();
            bool isError = Convert.ToBoolean(TestContext.DataRow["IsError"]);

            // Метод працює з глобальним або статичним полем expression
            AnalaizerClass.expression = inputExpr;

            // 2. Act
            ArrayList actualStack = null;
            string actualError = null;

            try
            {
                actualStack = AnalaizerClass.CreateStack();
            }
            catch (Exception ex)
            {
                actualError = ex.Message;
            }

            // 3. Assert
            if (isError)
            {
                // Якщо CreateStack записує помилку в стек, повертає рядок або кидає виняток
                string resultString = actualStack != null ? string.Join(" ", actualStack.ToArray()) : actualError;
                StringAssert.Contains(resultString, expected,
                    $"Вираз '{inputExpr}' мав повернути помилку {expected}");
            }
            else
            {
                Assert.IsNotNull(actualStack, $"Стек для '{inputExpr}' не повинен бути null");

                // Збираємо елементи ArrayList через пробіл для порівняння зі ЗПЗ
                string actualString = string.Join(" ", actualStack.ToArray());
                Assert.AreEqual(expected, actualString,
                    $"Некоректний ЗПЗ для виразу '{inputExpr}'");
            }
        }
    }
}