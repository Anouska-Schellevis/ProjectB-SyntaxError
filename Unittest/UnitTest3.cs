using System.Dynamic;

namespace Unittest;

[TestClass]
public class VoucherTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        VoucherAccess.ClearAllVouchers();
    }

    public void AddNewVoucher()
    {
        List<VoucherModel> testVouchers = new()
        {
            new(1, "QtTNU6G", "This is test one", 10, "percentage", null),
            new(2, "R6RvM8K", "This is test two", 10, "euro", null),
            new(3, "enYAXL5", "", 10.10, "percentage", null),
            new(4, "QtTNU6G", "This test has to go wrong", 10, "percentage", null),
            new(5, "YCyb3nt", "", 10, "hello", null),
        };

        foreach (VoucherModel testVoucher in testVouchers)
        {
            VoucherLogic.CreateVoucher(testVoucher);
        }

        var allVouchers = VoucherLogic.GetAllVouchers();

        Assert.AreEqual(1, allVouchers.Count(v => v.Id == 1), "Valid");
        Assert.AreEqual(1, allVouchers.Count(v => v.Id == 2), "Valid");

        Assert.AreEqual(0, allVouchers.Count(v => v.Id == 3), "Amount is not an int");
        Assert.AreEqual(0, allVouchers.Count(v => v.Id == 4), "Duplicate code");
        Assert.AreEqual(0, allVouchers.Count(v => v.Id == 5), "Type is incorrect");
    }
}