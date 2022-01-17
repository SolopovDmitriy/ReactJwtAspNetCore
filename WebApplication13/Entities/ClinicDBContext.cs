using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication13.Entities
{
    public class ClinicDBContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Navigate> Navigates { get; set; }
        public ClinicDBContext(DbContextOptions options) : base(options)
        {
           // Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=veterinary_clinic3;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>(entity => {
                entity.HasIndex(e => e.UrlSlug).IsUnique();
            });

            modelBuilder.Entity<Category>(entity => {
                entity.HasIndex(e => e.UrlSlug).IsUnique();
            });

            modelBuilder.Entity<Post>(entity => {
                entity.HasIndex(e => e.UrlSlug).IsUnique();
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.Login).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            User[] users = new User[]
            {
                new User()
                {
                    Id = 1, Login = "Admin", DateRegister = DateTime.Now, Email = "admin@admin.com", Password = "qwerty"
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            Employee[] employees = new Employee[]
            {
                new Employee {
                        Id = 1,
                        Fio = "Быков, Андрей Евгеньевич",
                        Speciality = "Заведующий терапевтическим отделением больницы и руководитель четырёх интернов. Самодур и циник. Является отличным специалистом, но обладает скверным характером.",
                        ImgSrc = "/images/employeers/buk.jpeg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 2,
                        Fio = "Купитман, Иван Натанович",
                        Speciality = "Доктор-дерматовенеролог, кандидат медицинских наук, заведующий кожно-венерологическим отделением больницы. Еврей, хитёр, корыстен. Ценитель дорогого коньяка. Лучший друг Быкова.",
                        ImgSrc = "/images/employeers/kupit.jpg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 3,
                        Fio = "Кисегач, Анастасия Константиновна",
                        Speciality = "Главный врач больницы, любовница, а затем жена Быкова. Женщина с жёстким характером, временами проявляет сентиментальность.",
                        ImgSrc = "/images/employeers/kisja.jpg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 4,
                        Fio = "Левин, Борис Аркадьевич",
                        Speciality = "«Ботаник» с красным дипломом, уверенный в собственной важности. Дотошный и нудный, из-за чего имеет мало друзей.",
                        ImgSrc = "/images/employeers/levin.jpg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 5,
                        Fio = "Романенко, Глеб Викторович",
                        Speciality = "Сын главного врача больницы Анастасии Кисегач от первого брака. Имеет репутацию «мажора», не пренебрегает возможностью разыграть своих коллег и отмазаться от работы. Лучший друг Лобанова.",
                        ImgSrc = "/images/employeers/roman.jpg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 6,
                        Fio = "Лобанов, Семён Семёнович",
                        Speciality = "Интерн, не самый умный и понимающий в медицине, но быстро соображающий как отпроситься с работы и заполучить халяву. Вспыльчив и нетерпелив, ненадёжен в плане долгов. Лучший друг Романенко.",
                        ImgSrc = "/images/employeers/loban.jpg",
                        ImgAlt = ""
                    },
                new Employee()
                    {
                        Id = 7,
                        Fio = "Черноус, Варвара Николаевна",
                        Speciality = "Исполнительная и инициативная, но иногда слишком наивная девушка. Не понимает намёков.",
                        ImgSrc = "/images/employeers/chern.jpg",
                        ImgAlt = ""
                    }
            };
            modelBuilder.Entity<Employee>().HasData(employees);

            Navigate[] navigates = new Navigate[]
           {
                new Navigate {
                    Id = 1,
                    Title = "Главная",
                    Href = "/Home/index",
                    Order = 1
                },
                new Navigate {
                    Id = 2,
                    Title = "О Нас",
                    Href = "/About/index",
                    Order = 5
                },
                new Navigate {
                    Id = 6,
                    Title = "Категории",
                    Href = "/Category/Index",
                    Order = 3
                },
                new Navigate {
                    Id = 7,
                    Title = "Форум",
                    Href = "/Post/Index",
                    Order = 4
                },
                new Navigate {
                    Id = 3,
                    Title = "Обратная связь",
                    Href = "#",
                    Order = 5
                },
                new Navigate {
                    Id = 4,
                    Title = "Связаться с нами",
                    Href = "/About/ContuctUs",
                    Order = 1,
                    ParentId = 3
                },
                new Navigate {
                    Id = 5,
                    Title = "Сотрудники",
                    Href = "/Employeer/Index",
                    Order = 2
                },
           };
            modelBuilder.Entity<Navigate>().HasData(navigates);

            Category[] categories = new Category[]
            {
                new Category {
                    Id = 1,
                    Title = "Новости",
                    Slogan = "Горячие ветеринарные новости",
                    Order = 1,
                    ImgSrc = "/images/categories/news.png",
                    ImgAlt = "Новости",
                    UrlSlug = "news-category",
                },
                new Category {
                    Id = 2,
                    Title = "Услуги",
                    Slogan = "Цивилизованные ветеринарные услуги в каждый дом и каждому питомцу",
                    Order = 2,
                    ImgSrc = "/images/categories/services.png",
                    ImgAlt = "Услуги",
                    UrlSlug = "services-category",
                    Content = "Услуги — предпринимательская деятельность, направленная на удовлетворение потребностей других лиц, за исключением деятельности, осуществляемой на основе трудовых правоотношений."
                },
                new Category {
                    Id = 3,
                    Title = "Достижения",
                    Slogan = "Новые и передовые подходы ветеринарной медицины",
                    Order = 3,
                    ImgSrc = "/images/categories/achievement.png",
                    ImgAlt = "Достижения",
                    UrlSlug = "achievement-category",
                }
            };
            modelBuilder.Entity<Category>().HasData(categories);

            Post[] posts = new Post[]
          {
                new Post() {
                    Id = 1,
                    CategoryId = 2,
                    ImgSrc = "/images/posts/1.jpg",
                    PostedOn = true,
                    Title = "Пальпация - новый и продвинутый метод обследования животных",
                    Slogan = "Физический метод медицинской диагностики, проводимый путём ощупывания тела пациента",
                    UrlSlug = "palpatio",
                    DateOfPublished = DateTime.Now
                },
                new Post() {
                    Id = 2,
                    CategoryId = 2,
                    ImgSrc = "/images/posts/2.jpg",
                    PostedOn = true,
                    Title = "Анализ шума как сумма различных по свойствам звуков, где нельзя выделить основной тон",
                    Slogan = "Перкуссия - метод медицинской диагностики, заключающийся в простукивании отдельных участков тела и анализе звуковых явлений, возникающих при этом.",
                    UrlSlug = "percussio",
                    DateOfPublished = DateTime.Now
                },
                new Post() {
                    Id = 3,
                    CategoryId = 2,
                    ImgSrc = "/images/posts/3.jpg",
                    PostedOn = true,
                    Title = "Термометрия — процедура измерения температуры тела.",
                    Slogan = "Исследование весьма простое и должно проводиться у каждого больного. Измерение температуры тела обычно производится медицинским максимальным термометром со шкалой, градуированной по Цельсию",
                    UrlSlug = "thermemetreo",
                    DateOfPublished = DateTime.Now,
                    Content = @"<p>Термометрия, обязательный метод клинического исследования, позволяет оценивать состояние животного, контролировать течение болезни, эффективность лечения, выявлять осложнения и прогнозировать развитие заболевания. При многих заболеваниях термометрия дает возможность выявить заболевание в продромальном периоде.</p><p>Заболеваниям свойственна определенная динамика температуры; например, при крупозной пневмонии важный симптом — постоянная пиретическая лихорадка, а при катаральной — ремит- тирующая субфебрильная.</p><p>Температуру тела измеряют ртутным максимальным термометром со шкалой Цельсия от 34 до 42 °С с делением по 0,1 <sup>в</sup>С. Ртутный столбик термометра, достигнув определенной высоты, удерживается на этом уровне и опускается лишь при встряхивании термометра. Применяют также электротермометр, которым можно быстро и с большой точностью измерить температуру. Измерения проводят в прямой кишке, у птиц — в клоаке. У самок можно измерять температуру во влагалище, где она выше, чем в прямой кишке, на 0,5 °С.</p><p>Крупные животные во время термометрии могут проявлять беспокойство. Чтобы избежать травм при измерении температуры, животное нужно фиксировать.

                        Перед введением термометр встряхивают, смазывают вазелином и осторожно вводят, поворачивая его вдоль продольной оси.

                        Термометр держат в прямой кишке 10 мин, фиксируя его зажимом за шерсть крупа. Потом термометр осторожно извлекают, обтирают, определяют температуру тела по шкале, встряхивают и помещают в банку с дезинфицирующим раствором.

                        При амбулаторном исследовании животных температуру тела измеряют однократно, а у находящихся под наблюдением — не менее двух раз в день: утром — от 7 до 9 ч и вечером — от 17 до 19 ч. При тяжелом состоянии больного, заразных болезнях температуру измеряют через каждые 2 ч.

                        Данные термометрии регистрируют записью в амбулаторном журнале, истории болезни и в виде графика, по которому можно судить о высоте, типе и продолжительности лихорадки.</p>"
                },
                new Post() {
                    Id = 4,
                    CategoryId = 2,
                    ImgSrc = "/images/posts/4.jpg",
                    PostedOn = true,
                    Title = "Аускультация  - новый и продвинутый метод обследования животных",
                    Slogan = "Физический метод медицинской диагностики, заключающийся в выслушивании звуков, образующихся в процессе функционирования внутренних органов",
                    UrlSlug = "auscultatio",
                    DateOfPublished = DateTime.Now
                },
                new Post() {
                    Id = 5,
                    CategoryId = 3,
                    ImgSrc = "/images/posts/5.jpg",
                    PostedOn = true,
                    Title = "Ветеринарная биотехнология",
                    Slogan = "Биотехнология использует микроорганизмы и вирусы, которые в процессе своей жизнедеятельности вырабатывают естественным путем необходимые нам вещества — витамины, ферменты, аминокислоты, органические кислоты, спирты, антибиотики и др. биологически активные соединения.",
                    UrlSlug = "biotechnology",
                    DateOfPublished = DateTime.Now
                },
                new Post() {
                    Id = 6,
                    CategoryId = 3,
                    ImgSrc = "/images/posts/6.jpg",
                    PostedOn = true,
                    Title = "Рекомбинантные вакцины",
                    Slogan = "производства этих вакцин применяют методы генной инженерии, встраивая генетический материал микроорганизма в дрожжевые клетки, продуцирующие антиген. После культивирования дрожжей из них выделяют нужный антиген, очищают и готовят вакцину. Примером таких вакцин может служить вакцина против гепатита В, а также вакцина против вируса папилломы человека",
                    UrlSlug = "vaccinus",
                    DateOfPublished = DateTime.Now
                },
                new Post() {
                    Id = 7,
                    CategoryId = 3,
                    ImgSrc = "/images/posts/7.jpg",
                    PostedOn = true,
                    Title = "Вакцины – антигены",
                    Slogan = "производства этих вакцин применяют методы генной инженерии, встраивая генетический материал микроорганизма в дрожжевые клетки, продуцирующие антиген. После культивирования дрожжей из них выделяют нужный антиген, очищают и готовят вакцину. Примером таких вакцин может служить вакцина против гепатита В, а также вакцина против вируса папилломы человека",
                    UrlSlug = "vaccinus-antigens",
                    DateOfPublished = DateTime.Now
                },
          };
            modelBuilder.Entity<Post>().HasData(posts);
        }
    }
}
