using System.Dynamic;
using Microsoft.Data.Sqlite;


namespace UnittestVoucher;

[TestClass]
public class VoucherTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        VoucherAccess.ClearAllVouchers();
    }

    [TestMethod]
    public void AddCorrectVoucher()
    {
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

    [TestMethod]
    public void GetVouchersForUser1()
    {
        List<VoucherModel> testVouchers = new()
        {
            new(1, "16dFecD", "This is the first test", 20m, "percentage", 1),
            new(2, "OcjBkca", "This is the second test", 13m, "euro", 1),
            new(3, "yitrFeF", "This is the third test", 33m, "euro", 3)
        };

        foreach (VoucherModel testVoucher in testVouchers)
        {
            VoucherLogic.CreateVoucher(testVoucher);
        }

        var allUserVouchers = VoucherLogic.GetVouchersByUserId(1);

        Assert.IsTrue(allUserVouchers.All(v => v.UserId == 1));
    }

    public void CalcDiscountedPrice_PerSeatByCategory_ForUser1()
    {
        List<VoucherModel> testVouchers = new()
        {
            new(1, "16dFecD", "This is the first test", 20m, "percentage", 1),
            new(2, "OcjBkca", "This is the second test", 13m, "euro", 1),
            new(3, "yitrFeF", "This is the third test", 33m, "euro", 3)
        };

        foreach (VoucherModel testVoucher in testVouchers)
        {
            VoucherLogic.CreateVoucher(testVoucher);
        }

        var allUserVouchers = VoucherLogic.GetVouchersByUserId(1);
    }
}