using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities.Base
{
    /// <summary>
    /// Response trả về từ Service
    /// </summary>
    /// Created By: NTHIEU (12/06/2021)
    public class ActionServiceResult
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// Created By: NTHIEU (12/06/2021)
        public ActionServiceResult()
        {

        }
        /// <summary>
        /// - Hàm khởi tạo có các tham số truyền vào
        /// - Không có dữ liệu trả về
        /// </summary>
        /// <param name="statusCode">Mã request</param>
        /// <param name="userMsg">Thông báo cho người dùng</param>
        /// <param name="devMsg">Thông báo cho Dev</param>
        /// Created By: NTHIEU (12/06/2021)
        public ActionServiceResult(int statusCode, string userMsg, string devMsg)
        {
            this.StatusCode = statusCode;
            this.UserMsg = userMsg;
            this.DevMsg = devMsg;
        }

        /// <summary>
        /// - Hàm khởi tạo có các tham số truyền vào
        /// - Có dữ liệu trả về
        /// </summary>
        /// <param name="statusCode">Mã request</param>
        /// <param name="userMsg">Thông báo cho người dùng</param>
        /// <param name="devMsg">Thông báo cho Dev</param>
        /// <param name="data">Dữ liệu trả về</param>
        /// Created By: NTHIEU (12/06/2021)
        public ActionServiceResult(int statusCode, string userMsg, string devMsg, object data)
        {
            this.StatusCode = statusCode;
            this.UserMsg = userMsg;
            this.DevMsg = devMsg;
            this.Data = data;
        }


        /// <summary>
        /// Mã request trả về
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông báo cho Lập trình viên
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Đối tượng trả về
        /// </summary>
        public object Data { get; set; }
    }
}
