using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopupLoading.Enum;
public class PopupLoadingModel
{
    public List<TypeResponse> types;
    public string content;
    public PopupLoadingModel() { }
    public PopupLoadingModel(List<TypeResponse> types, string content)
    {
        this.types = types;
        this.content = content;
    }
    public bool IsTypeHaveTheSame(PopupLoadingModel they)
    {
        return types.Any(t => they.types.Contains(t));
    }
}
