using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class VoucherLogic
{
    public VoucherLogic()
    {
        // Could do something here

    }

    static public void CreateVoucher(VoucherModel voucher)
    {
        VoucherAccess.Write(voucher);
    }

    public VoucherModel GetById(int id)
    {
        return VoucherAccess.GetById(id);
    }

    public VoucherModel GetByType(string type)
    {
        return VoucherAccess.GetByType(type);
    }

    static public List<VoucherModel> GetAllVouchers()
    {
        return VoucherAccess.GetAllVouchers();
    }
}




