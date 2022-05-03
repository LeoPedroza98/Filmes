export class ArquivoService {
  async Ler(arquivo: any): Promise<string> {
    return new Promise((resolve, reject) => {
      let fr = new FileReader();

      fr.onload = (arquivo: any) => {
        resolve(arquivo!.target!.result);
      };

      fr.readAsDataURL(arquivo);
    });
  }
  async Download(arquivo: any, tipo: string, nome: string) {
    const a = document.createElement("a");
    a.href = await this.Ler(arquivo)
    a.type = tipo;
    a.download = nome;
    a.click();
    a.remove();
  }  
}