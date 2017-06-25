namespace Conetados.Webapi.Migrations
{
    using Models;
    using Services;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;

    internal sealed class Configuration : DbMigrationsConfiguration<Conetados.Webapi.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto context)
        {
            //Debug Migrations
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            #region Usuarios
            //context.Usuarios.AddOrUpdate(
            //  p => p.Id,
            //  new Usuario()
            //  {
            //      Id = 1,
            //      Nome = "Rafael Liendo"
            //  }
            //);
            #endregion

            #region Uploads
            //fetch dummy photo
            WebClient wc = new WebClient();
            var imageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data/image-placeholder.jpg");
            FileStream imageStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read);
            Upload imagePlaceholder = ImageService.ResizeAndCompress(imageStream);
            Upload thumbnailPlaceholder = ThumbnailService.CreateThumbnail(imageStream);

            context.Uploads.AddOrUpdate(
              p => p.Id,
              new Upload()
              {
                  Id = 1,
                  Blob = imagePlaceholder.Blob,
                  MediaType = imagePlaceholder.MediaType,
                  Width= imagePlaceholder.Width,
                  Height = imagePlaceholder.Height
              }
            );
            #endregion

            #region Tags
            context.Tags.AddOrUpdate(
              p => p.Id,
              new Tag
              {
                  Id = 1,
                  Nome = "Banners",
                  Descrição = "",
                  Editoria = false
              },
              new Tag
              {
                  Id = 2,
                  Nome = "Destaques",
                  Descrição = "",
                  Editoria = false
              },              
              new Tag
              {
                  Id = 3,
                  Nome = "Enquetes",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 4,
                  Nome = "Duraseg",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 5,
                  Nome = "Duratex no Mundo",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 6,
                  Nome = "Fique por dentro",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 7,
                  Nome = "Você sabia?",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 8,
                  Nome = "Nossa gente",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 9,
                  Nome = "Nossas unidades",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 10,
                  Nome = "Nossos produtos",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 11,
                  Nome = "Nossos resultados",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 12,
                  Nome = "Reconhecimentos",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 13,
                  Nome = "Imagens",
                  Descrição = "",
                  Editoria = true
              },
              new Tag
              {
                  Id = 14,
                  Nome = "Vídeos",
                  Descrição = "",
                  Editoria = true
              }
            );
            #endregion

            #region Artigos e Layouts
            context.ArtigosBase.AddOrUpdate(
              p => p.Id,
            #region Layouts sem enquete
              new Layout()
              {
                  Id = 1,
                  Titulo = "Layout titulo e texto",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 2,
                  Titulo = "Layout imagem, título e texto",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 3,
                  Titulo = "Layout título, texto e vídeo",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 4,
                  Titulo = "Layout título, texto e galeria",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 5,
                  Titulo = "Layout imagem e título",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 6,
                  Titulo = "Layout título e vídeo",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 7,
                  Titulo = "Layout título e galeria",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
            #endregion

            #region Layouts com enquete
              new Layout()
              {
                  Id = 8,
                  Titulo = "Layout titulo, texto e enquete",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 9,
                  Titulo = "Layout imagem, título, texto e enquete",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              },
              new Layout()
              {
                  Id = 10,
                  Titulo = "Layout imagem, título e enquete",
                  UsuarioCmsId = "9RLIENDO",
                  UsuarioCmsNome = "Rafael Liendo",
              }
            #endregion
            #region Artigos
              //new Artigo()
              //{
              //    Id = 11,
              //    LayoutId = 2,
              //    Titulo = "Hello World!",
              //    UsuarioCmsId = "9RLIENDO",
              //    UsuarioCmsNome = "Rafael Liendo",
              //    Thumbnail = thumbnail,
              //    Ativo = false,
              //    Tags = new int[] { 1, 2, 6 }.Select(x => context.Tags.Find(x)).ToList(),
              //    DataPublicacao = DateTime.Now
              //}
            #endregion
            );
            #endregion;

            #region Conteudos
            context.Conteudos.AddOrUpdate(
              p => p.Id,
            #region Conteudo de layout sem enquete
            #region Layout titulo e texto
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 1,
                  ArtigoId = 1,
                  Ordem = 1,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Paragrafo,
                  Id = 2,
                  ArtigoId = 1,
                  Ordem = 2,
                  TextoParagrafo = string.Empty
              },
            #endregion

            #region Layout imagem, título e texto
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Imagem,
                  Id = 3,
                  ArtigoId = 2,
                  Ordem = 1,
                  Descricao = string.Empty,
                  UploadId = 1
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 4,
                  ArtigoId = 2,
                  Ordem = 2,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Paragrafo,
                  Id = 5,
                  ArtigoId = 2,
                  Ordem = 3,
                  TextoParagrafo = string.Empty
              },
            #endregion

            #region Layout título, texto e vídeo
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 6,
                  ArtigoId = 3,
                  Ordem = 1,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Paragrafo,
                  Id = 7,
                  ArtigoId = 3,
                  Ordem = 2,
                  TextoParagrafo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Video,
                  Id = 8,
                  ArtigoId = 3,
                  Ordem = 3,
                  YoutubeVideoId = string.Empty
              },
            #endregion

            #region Layout título, texto e galeria
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 9,
                  ArtigoId = 4,
                  Ordem = 1,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Paragrafo,
                  Id = 10,
                  ArtigoId = 4,
                  Ordem = 2,
                  TextoParagrafo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Galeria,
                  Id = 11,
                  ArtigoId = 4,
                  Ordem = 3
              },
              
            #endregion

            #region Layout imagem e título
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Imagem,
                  Id = 12,
                  ArtigoId = 5,
                  Ordem = 1,
                  Descricao = string.Empty,
                  UploadId = 1
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 13,
                  ArtigoId = 5,
                  Ordem = 2,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
            #endregion

            #region Layout título e vídeo
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 14,
                  ArtigoId = 6,
                  Ordem = 1,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Video,
                  Id = 15,
                  ArtigoId = 6,
                  Ordem = 2,
                  YoutubeVideoId = string.Empty
              },
            #endregion

            #region Layout título e galeria
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 16,
                  ArtigoId = 7,
                  Ordem = 1,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Galeria,
                  Id = 17,
                  ArtigoId = 7,
                  Ordem = 2
              },              
            #endregion
            #endregion

            #region Conteudo de layout com enquete
            #region Layout titulo, texto e enquete
            new Conteudo()
            {
                TipoConteudo = TipoConteudo.Titulo,
                Id = 18,
                ArtigoId = 8,
                Ordem = 1,
                Importancia = 1,
                TextoTitulo = string.Empty
            },
            new Conteudo()
            {
                TipoConteudo = TipoConteudo.Paragrafo,
                Id = 19,
                ArtigoId = 8,
                Ordem = 2,
                TextoParagrafo = string.Empty
            },
            new Conteudo()
            {
                TipoConteudo = TipoConteudo.Enquete,
                Id = 20,
                ArtigoId = 8,
                Ordem = 3
            },
            #endregion

            #region Layout imagem, título, texto e enquete
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Imagem,
                  Id = 21,
                  ArtigoId = 9,
                  Ordem = 1,
                  Descricao = string.Empty,
                  UploadId = 1
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 22,
                  ArtigoId = 9,
                  Ordem = 2,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Paragrafo,
                  Id = 23,
                  ArtigoId = 9,
                  Ordem = 3,
                  TextoParagrafo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Enquete,
                  Id = 24,
                  ArtigoId = 9,
                  Ordem = 4
              },
            #endregion

            #region Layout imagem, título e enquete
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Imagem,
                  Id = 25,
                  ArtigoId = 10,
                  Ordem = 1,
                  Descricao = string.Empty,
                  UploadId = 1
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Titulo,
                  Id = 26,
                  ArtigoId = 10,
                  Ordem = 2,
                  Importancia = 1,
                  TextoTitulo = string.Empty
              },
              new Conteudo()
              {
                  TipoConteudo = TipoConteudo.Enquete,
                  Id = 27,
                  ArtigoId = 10,
                  Ordem = 3
              }
              #endregion
              #endregion

              #region Conteudo de artigos
              //new Conteudo()
              //{
              //    TipoConteudo = TipoConteudo.Imagem,
              //    Id = 28,
              //    ArtigoId = 11,
              //    Ordem = 1,
              //    Descricao = "Descrição da imagem",
              //    UploadId = 1
              //},
              //new Conteudo()
              //{
              //    TipoConteudo = TipoConteudo.Titulo,
              //    Id = 29,
              //    ArtigoId = 11,
              //    Ordem = 2,
              //    Importancia = 1,
              //    TextoTitulo = "Layout imagem título e texto"
              //},
              //new Conteudo()
              //{
              //    TipoConteudo = TipoConteudo.Paragrafo,
              //    Id = 30,
              //    ArtigoId = 11,
              //    Ordem = 3,
              //    TextoParagrafo = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi at ante efficitur, molestie massa eget, eleifend odio. Praesent congue sapien non erat elementum iaculis. Fusce rutrum turpis leo, non sagittis purus bibendum non. Vestibulum posuere aliquet nisi, at porta odio accumsan sed. Fusce eu fringilla justo, in mollis nisi. Suspendisse a convallis felis, sed congue neque. Pellentesque viverra mi sit amet orci tincidunt, ac blandit ligula vulputate. Aenean commodo faucibus lectus, et cursus velit mattis vitae. In arcu purus, faucibus id malesuada sit amet, scelerisque non enim. Donec tincidunt lectus purus, pulvinar vulputate urna faucibus in. Aliquam id fringilla neque, et suscipit eros. Duis non dolor id lacus tempor ullamcorper. Proin nec scelerisque enim. Ut molestie magna sed leo viverra tristique.</p><p>Pellentesque molestie ante quis sollicitudin maximus. Vivamus vitae elit sit amet tellus mollis posuere. Duis vitae est maximus, consequat tellus id, tincidunt eros. Interdum et malesuada fames ac ante ipsum primis in faucibus. Proin urna quam, euismod ultrices auctor quis, egestas in lorem. Vivamus faucibus iaculis dignissim. Integer ac nunc in leo pharetra sodales. Integer feugiat faucibus semper. Vestibulum facilisis hendrerit dui. Suspendisse eu nulla vel augue gravida ornare id vulputate quam. Praesent blandit aliquam libero vitae tincidunt. Morbi non erat erat. Vestibulum ac erat eleifend, ullamcorper odio non, porttitor purus. Maecenas ligula elit, sagittis id nisl congue, sagittis dapibus dolor.</p><p>Aliquam ultricies sem at erat sagittis varius. Morbi ut enim imperdiet, imperdiet dolor nec, venenatis mauris. Sed nec commodo sapien. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam in eleifend metus, ut mattis odio. Sed ac rhoncus ligula. Integer a consectetur ante.</p><p>Morbi enim enim, finibus a est quis, vestibulum luctus nisi. Ut ullamcorper in lectus sed porta. Nunc nec consequat dolor, non elementum nibh. Sed auctor fringilla sagittis. Vivamus in diam lorem. Aliquam sapien lorem, volutpat id metus blandit, lacinia auctor tortor. Quisque nibh nisi, ullamcorper ut erat vitae, dictum congue libero. Phasellus nec ante volutpat, gravida sem in, suscipit neque. Suspendisse lacinia convallis quam, eget pharetra quam facilisis pretium. Donec vitae condimentum nulla, et ultricies tellus. Praesent eget neque facilisis sapien facilisis vestibulum ac eget tellus. Vestibulum tristique feugiat laoreet. Aliquam quis tellus dictum sem fermentum molestie id et justo. Phasellus ut elementum lorem. Cras eu faucibus turpis. Nulla egestas sapien turpis, quis tristique nunc condimentum non.</p><p>Etiam ultrices, purus vitae maximus dapibus, risus elit feugiat nisi, vel facilisis mi turpis sed lacus. Aenean facilisis nisl nec sapien rhoncus, vitae gravida nulla finibus. Sed ut quam condimentum, aliquam ex pretium, finibus nulla. Integer porta eros purus, ac condimentum eros maximus pharetra. Ut nec ex nec risus suscipit ullamcorper. Fusce convallis imperdiet nisl in bibendum. Nunc tempor turpis vitae aliquet faucibus. Morbi quis tempor sem.</p>"
              //}
              #endregion
            );

            #endregion;

            #region ImagemGaleria
            //context.ImagensGaleria.AddOrUpdate(
            //    p => p.Id,
            //    new ImagemGaleria()
            //    {
            //        Id = 12,
            //        Ordem = 1,
            //        GaleriaId = 11,
            //        Descricao = "Imagem de galeria 1",
            //        UploadId = 1
            //    },
            //  new ImagemGaleria()
            //  {
            //      Id = 13,
            //      Ordem = 2,
            //      GaleriaId = 11,
            //      Descricao = "Imagem de galeria 2",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 14,
            //      Ordem = 3,
            //      GaleriaId = 11,
            //      Descricao = "Imagem de galeria 3",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 15,
            //      Ordem = 4,
            //      GaleriaId = 11,
            //      Descricao = "Imagem de galeria 4",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 16,
            //      Ordem = 5,
            //      GaleriaId = 11,
            //      Descricao = "Imagem de galeria 5",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 23,
            //      Ordem = 1,
            //      GaleriaId = 17,
            //      Descricao = "Imagem de galeria 1",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 24,
            //      Ordem = 2,
            //      GaleriaId = 17,
            //      Descricao = "Imagem de galeria 2",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 25,
            //      Ordem = 3,
            //      GaleriaId = 17,
            //      Descricao = "Imagem de galeria 3",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 26,
            //      Ordem = 4,
            //      GaleriaId = 17,
            //      Descricao = "Imagem de galeria 4",
            //      UploadId = 1
            //  },
            //  new ImagemGaleria()
            //  {
            //      Id = 27,
            //      Ordem = 5,
            //      GaleriaId = 17,
            //      Descricao = "Imagem de galeria 5",
            //      UploadId = 1
            //  }
            //); 
            #endregion

            #region Alternativas
            //context.Alternativas.AddOrUpdate(
            //  p => p.Id,
            //  new Alternativa()
            //  {
            //      Id = 1,
            //      Descricao = "Alternativa 1",
            //      EnqueteId = 20
            //  },
            //  new Alternativa()
            //  {
            //      Id = 2,
            //      Descricao = "Alternativa 2",
            //      EnqueteId = 20
            //  },
            //  new Alternativa()
            //  {
            //      Id = 3,
            //      Descricao = "Alternativa 3",
            //      EnqueteId = 20
            //  },
            //  new Alternativa()
            //  {
            //      Id = 4,
            //      Descricao = "Alternativa 1",
            //      EnqueteId = 24
            //  },
            //  new Alternativa()
            //  {
            //      Id = 5,
            //      Descricao = "Alternativa 2",
            //      EnqueteId = 24
            //  },
            //  new Alternativa()
            //  {
            //      Id = 6,
            //      Descricao = "Alternativa 3",
            //      EnqueteId = 24
            //  },
            //  new Alternativa()
            //  {
            //      Id = 7,
            //      Descricao = "Alternativa 1",
            //      EnqueteId = 27
            //  },
            //  new Alternativa()
            //  {
            //      Id = 8,
            //      Descricao = "Alternativa 2",
            //      EnqueteId = 27
            //  },
            //  new Alternativa()
            //  {
            //      Id = 9,
            //      Descricao = "Alternativa 3",
            //      EnqueteId = 27
            //  }
            //);
            #endregion

            #region Curtidas
            //context.Curtidas.AddOrUpdate(
            //  p => new { p.UsuarioAppId, p.ArtigoId },
            //  new Curtida
            //  {
            //      ArtigoId = 11,
            //      UsuarioAppId = "9RLIENDO",
            //      UsuarioAppNome =  "Rafael Liendo"
            //  }
            //);
            #endregion

            #region Comentários
            //context.Comentarios.AddOrUpdate(
            //  p => p.Id,
            //  new Comentario
            //  {
            //      Id = 1,
            //      ArtigoId = 11,
            //      UsuarioAppId = "9RLIENDO",
            //      UsuarioAppNome = "Rafael Liendo",
            //      Mensagem = "Hello World!"
            //  }
            //);
            #endregion
        }
    }
}
