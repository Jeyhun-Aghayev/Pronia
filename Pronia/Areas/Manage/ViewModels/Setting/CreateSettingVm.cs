using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Manage.ViewModels.Setting
{
	public class CreateSettingVm
	{
		[Required(ErrorMessage = "Key daxil edilmelidir")]
		[MaxLength(20, ErrorMessage = "Maksimum 20 herf olmalıdır.")]
		public string Key { get; set; }
		[Required(ErrorMessage = "Value daxil edilmelidir")]
		public string Value { get; set; }
	}
}
