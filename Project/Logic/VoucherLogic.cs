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
        if (voucher.Type != "percentage" && voucher.Type != "euro")
        {
            throw new ArgumentException("Invalid voucher type.");
        }
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

    static public List<VoucherModel> GetVouchersByUserId(long userId)
    {
        return VoucherAccess.GetVouchersByUserId(userId);
    }
    
    static public double PriceIncludingVoucherDiscount(VoucherModel voucher, double seatPrice)
    {
        if (voucher.Type == "percentage")
        {
            double discountPrice = seatPrice / 100 * (double)voucher.Amount;
            return seatPrice - discountPrice;
        }
        if (voucher.Type == "euro")
        {
            if (seatPrice < (double)voucher.Amount)
            {
                voucher.Amount -= (decimal)seatPrice;
                return 0;
            }

            return seatPrice - (double)voucher.Amount;
        }

        return seatPrice;
    }
}




