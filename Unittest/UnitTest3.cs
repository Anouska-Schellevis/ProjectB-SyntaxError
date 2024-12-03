using System.Dynamic;
using Microsoft.Data.Sqlite;


namespace UnittestVoucher;

[TestClass]
public class VoucherTest
{
    [TestMethod]
    public void AddCorrectVoucher()
    {
        VoucherAccess.ClearAllVouchers();

        List<VoucherModel> testVouchers = new()
        {
            new(1, "QtTNU6G", "This is test one", 10m, "percentage", null),
            new(2, "R6RvM8K", "This is test two", 10.10m, "euro", null),
        };

        foreach (VoucherModel testVoucher in testVouchers)
        {
            VoucherLogic.CreateVoucher(testVoucher);
        }

        var allVouchers = VoucherLogic.GetAllVouchers();

        Assert.AreEqual(1, allVouchers.Count(v => v.Id == 1), "Valid");
        Assert.AreEqual(1, allVouchers.Count(v => v.Id == 2), "Valid");
    }

    [TestMethod]
    public void Incorrect_Type()
    {
        VoucherAccess.ClearAllVouchers();

        var testVoucher = new VoucherModel(1, "YCyb3nt", "", 10m, "hello", null);

        bool exceptionThrown = false;

        try
        {
            VoucherLogic.CreateVoucher(testVoucher);
            Assert.Fail("Invalid voucher type");
        }
        catch (ArgumentException ex)
        {
            exceptionThrown = true;

            Assert.IsTrue(ex.Message.Contains("Invalid voucher type"), "Invalid");
        }

        Assert.IsTrue(exceptionThrown, "no exception");

        var allVouchers = VoucherLogic.GetAllVouchers();
        Assert.AreEqual(0, allVouchers.Count(v => v.Code == "YCyb3nt"), "Valid");
    }

    [TestMethod]
    public void Duplicate_Code()
    {
        VoucherAccess.ClearAllVouchers();

        var testVoucher = new VoucherModel(1, "QtTNU6G", "This is test one", 10m, "percentage", null);
        VoucherLogic.CreateVoucher(testVoucher);

        var incorrectTestVoucher = new VoucherModel(2, "QtTNU6G", "This test has to go wrong", 10m, "percentage", null);

        try
        {
            VoucherLogic.CreateVoucher(incorrectTestVoucher);
            Assert.Fail("Test failed");
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"Hey: {ex.Message}");
            Assert.IsTrue(ex.Message.Contains("UNIQUE constraint failed: voucher.code"));
        }

        var allVouchers = VoucherLogic.GetAllVouchers();
        Assert.AreEqual(1, allVouchers.Count(v => v.Code == "QtTNU6G"), "Valid");
        // Assert.AreEqual(0, allVouchers.Count(v => v.Id == 2), "Amount is not an int");
    }
}