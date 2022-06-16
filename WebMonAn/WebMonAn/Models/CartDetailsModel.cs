using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonAn.Models
{
    [Table("CartDetails")]
    public class CartDetailsModel
    {
        [Key]
        public int CartDetailsID { get; set; }

        [ForeignKey("cartModel")]
        public int CartID { get; set; }

        [ForeignKey("foodModel")]
        public int MonAnId { get; set; }

        [Display(Name = "Món Ăn")]
        [StringLength(50)]
        public string TenMonAn { get; set; }

        [Display(Name = "Giá")]
        [Column(TypeName = "Money")]
        public double GiaMonAn { get; set; }

        [Display(Name = "Hình Ảnh")]
        [StringLength(250)]
        public string Hinh { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "Bạn cần nhập số lượng")]
        [Display(Name = "Số Lượng")]
        public int Soluong { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập thành tiền")]
        [Display(Name = "Thành Tiền")]
        public double ThanhTien { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi Chú")]
        public string GhiChu { get; set; }

        public CartModel cartModel { get; set; }
        public FoodModel foodModel { get; set; }
    }
}
