using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{

    //when recive payment from client
    public enum OrderStatus
    {
        //we want to get the text not number
        [EnumMember(Value="Pending")]
        Pending,
        [EnumMember(Value = "PayementRecived")]
        PayementRecived,
        [EnumMember(Value = "PayementFaild")]
        PaymentFaild,
    }
}
