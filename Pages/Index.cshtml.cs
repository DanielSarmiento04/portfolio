using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PortFolio.Services;

namespace PortFolio.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private MicrosoftTranslator _translator;
    [BindProperty]
    public string TextIndex { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _translator = new MicrosoftTranslator();
    }

    public async Task OnGetAsync()
    {
        TextIndex = await _translator.Translate("            Programador, ingeniero mecánico y emprendedor, mi objetivo es mejorar la calidad de vida de las personas, la tecnología es la herramienta que utilizo. Tengo experiencia en el desarrollo de aplicaciones web y de escritorio y en el análisis de datos.");
    }
    public void OnPost()
    {

    }
}
