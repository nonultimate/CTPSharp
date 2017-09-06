using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 证件类型
    /// </summary>
    [Serializable]
    public enum IdCardType
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        EID = '0',

        /// <summary>
        /// 身份证
        /// </summary>
        IDCard = '1',

        /// <summary>
        /// 军官证
        /// </summary>
        OfficerIDCard = '2',

        /// <summary>
        /// 警官证
        /// </summary>
        PoliceIDCard = '3',

        /// <summary>
        /// 士兵证
        /// </summary>
        SoldierIDCard = '4',

        /// <summary>
        /// 户口簿
        /// </summary>
        HouseholdRegister = '5',

        /// <summary>
        /// 护照
        /// </summary>
        Passport = '6',

        /// <summary>
        /// 台胞证
        /// </summary>
        TaiwanCompatriotIDCard = '7',

        /// <summary>
        /// 回乡证
        /// </summary>
        HomeComingCard = '8',

        /// <summary>
        /// 营业执照号
        /// </summary>
        LicenseNo = '9',

        /// <summary>
        /// 税务登记号
        /// </summary>
        TaxNo = 'A',

        /// <summary>
        /// 其他证件
        /// </summary>
        OtherCard = 'x'
    }
}
