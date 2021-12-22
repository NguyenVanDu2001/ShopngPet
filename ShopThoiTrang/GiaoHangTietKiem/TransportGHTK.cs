using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopThoiTrang.GiaoHangTietKiem
{
    interface TransportGHTK
    {
        string getFree();
        string getStatusOrder();
        bool updateOrderToGHTK();
        bool canelOrder();
        void PrintOrder();
        string getAddressAllOrder();
    }
}
