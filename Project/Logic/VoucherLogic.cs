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
    static public void UpdateVoucher(VoucherModel voucher)
    {
        VoucherAccess.Update(voucher);
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

    static public List<VoucherModel> GetVouchersByUserId(UserModel user)
    {
        return VoucherAccess.GetVouchersByUserId(user);
    }
    
    static public decimal CalculateDiscountedPrice(ref VoucherModel voucher, decimal price)
    {
        if (voucher.Type == "percentage")
        {
            decimal discountPrice = price / 100 * voucher.Amount;

            // voucher doesn't have a value anymore
            voucher.Amount = 0;

            return price - discountPrice;
        }
        else if (voucher.Type == "euro")
        {
            if (price < voucher.Amount)
            {
                // The value of the voucher drops
                voucher.Amount -= price;

                return 0; // Since the voucher is worth more than the regarding price, the discounted price will always be 0
            }

            decimal newPrice = price - voucher.Amount;

            // voucher doesn't have a value anymore
            voucher.Amount = 0;
            
            return newPrice;
        }
        else
        {
            return price;
        }
    }
}




