using MISA.Core.Entities.Base;
using MISA.Core.Entities.Directory;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Infrastructures;
using MISA.Core.Interfaces.Services;
using MISA.Core.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        #endregion

        #region Method Implement
        public async Task<ActionServiceResult> GetEmployeesPagingFilter(int pageIndex, int pageSize, string dataFilter)
        {
            var employees = await _employeeRepository.GetEmployeePagingFilter(pageIndex, pageSize, dataFilter);
            var totalEmployees = await _employeeRepository.GetTotalEmployeesFilter(dataFilter);
            if (employees.Count() > 0)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = new {
                        employees = employees,
                        total = totalEmployees
                    }
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 202,
                    UserMsg = "Không có dữ liệu",
                    DevMsg = "Không có dữ liệu",
                    Data = null,
                };
                return res;
            }

        }

        public async Task<ActionServiceResult> GetNewEmployee()
        {
            var employee = new Employee();
            employee.EmployeeCode = await _employeeRepository.GetNewEmployeeCode();
            if (employee != null)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = employee,
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 202,
                    UserMsg = "Không có dữ liệu",
                    DevMsg = "Không có dữ liệu",
                    Data = null,
                };
                return res;
            }

        }
        public async Task<ActionServiceResult> GetDuplicateEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetById(id);

            //2. lấy mã code mới
            employee.EmployeeCode = await _employeeRepository.GetNewEmployeeCode();
            if (employee != null)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = employee,
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu không thành công",
                    DevMsg = "Load dữ liệu không thành công",
                    Data = null,
                };
                return res;
            }
        }

        public async Task<Stream> ExportEmployees()
        {
            // 1. lấy đối tượng trên database
            var employees = await _employeeRepository.GetAll();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Add("Danh sách nhân viên");

            //1. Thêm title và style cho nó (title được đặt vào dòng đầu tiên luôn)
            using (ExcelRange range = worksheet.Cells["A1:I1"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Merge = true;
                range.Value = "Danh sách nhân viên";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 18;
                worksheet.Row(1).Height = 22;
                worksheet.Row(2).Height = 22;
            };
            //2. merge các ô ở dòng thứ 2
            using (ExcelRange range = worksheet.Cells["A2:I2"])
            {
                range.Merge = true;
            };

            //3. style header title
            using (ExcelRange range = worksheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                // Thêm border cho từng ô
                for(int j=1; j <= 9; j++)
                {
                    worksheet.Cells[3, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
            }
            //3.1 Tạo header title cho từng cột
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.Sort)].Value = ExcelResources.Sort;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.EmployeeCode)].Value = ExcelResources.EmployeeCode;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.FullName)].Value = ExcelResources.FullName;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.Gender)].Value = ExcelResources.GenderName;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.DateOfBirth)].Value = ExcelResources.DateOfBirth;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.Position)].Value = ExcelResources.Position;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.DepartmentName)].Value = ExcelResources.DepartmentName;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.AccountNumber)].Value = ExcelResources.AccountNumber;
            worksheet.Cells[3, (int)(ExcelEmployeeColumn.BankName)].Value = ExcelResources.BankName;
            worksheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Row(3).Style.Font.Bold = true;



            //4. Set độ rộng cho các cột
            worksheet.Column((int)(ExcelEmployeeColumn.Sort)).Width = 5;
            worksheet.Column((int)(ExcelEmployeeColumn.EmployeeCode)).Width = 15;
            worksheet.Column((int)(ExcelEmployeeColumn.FullName)).Width = 30;
            worksheet.Column((int)(ExcelEmployeeColumn.Gender)).Width = 15;
            worksheet.Column((int)(ExcelEmployeeColumn.DateOfBirth)).Width = 15;
            worksheet.Column((int)(ExcelEmployeeColumn.Position)).Width = 25;
            worksheet.Column((int)(ExcelEmployeeColumn.DepartmentName)).Width = 30;
            worksheet.Column((int)(ExcelEmployeeColumn.AccountNumber)).Width = 20;
            worksheet.Column((int)(ExcelEmployeeColumn.BankName)).Width = 30;

            //5. Set Style riêng cho các hàng
            worksheet.Column((int)(ExcelEmployeeColumn.Sort)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column((int)(ExcelEmployeeColumn.EmployeeCode)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column((int)(ExcelEmployeeColumn.Gender)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column((int)(ExcelEmployeeColumn.DateOfBirth)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column((int)(ExcelEmployeeColumn.AccountNumber)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            worksheet.Column((int)(ExcelEmployeeColumn.Position)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column((int)(ExcelEmployeeColumn.BankName)).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            //5. Gán từng giá trị của nhân viên cho excel
            int i = 0;
            foreach (var employee in employees)
            {
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.Sort].Value = i + 1;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.EmployeeCode].Value = employee.EmployeeCode;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.FullName].Value = employee.FullName;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.Gender].Value = employee.GenderName;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.DateOfBirth].Style.Numberformat.Format = "dd/mm/yyyy";
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.DateOfBirth].Formula = FomatDateTimeToFormulaDateInExcel(employee.DateOfBirth); 
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.Position].Value = employee.PositionName;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.DepartmentName].Value = employee.DepartmentName;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.AccountNumber].Value = employee.AccountNumber;
                worksheet.Cells[4 + i, (int)ExcelEmployeeColumn.BankName].Value = employee.BankName;
                // Thêm border cho từng ô
                for (int j = 1; j <= 9; j++)
                {
                    worksheet.Cells[4 + i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                i++;
            }
            package.Save();
            stream.Position = 0;
            return stream;
        }
        #endregion

        #region Method Not Emplement
        /// <summary>
        /// Xử lý format kiểu định dạng ngày tháng năm cho excel
        /// </summary>
        /// <param name="moment"></param>
        /// <returns>
        /// string(dd/mm/yyyy)
        /// </returns>
        public string FomatDateTimeToFormulaDateInExcel(DateTime? moment)
        {
            if (moment != null)
            {
                var dd = moment.Value.Day;
                var mm = moment.Value.Month;
                var yyyy = moment.Value.Year;
                return $"=DATE({dd},{mm},{yyyy})";
            }
            else return "";
            
        }
        #endregion
    }
}
