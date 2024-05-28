
using Microsoft.EntityFrameworkCore;
using SonarQubeUseCase.Data;
using System.Runtime.InteropServices;

namespace SonarQubeUseCase;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=database.db"));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/api/getuser", (ApplicationDbContext db, string userId) =>
        {
            // burada SQL injection yapilabilir
            var query = $"SELECT * FROM Users WHERE UserId = {userId}";
            var users = db.Users.FromSqlRaw(query).ToListAsync();
            return users;
        });

        app.MapGet("/api/rezilkod", (ApplicationDbContext db) =>
        {
            string unusedVariable = ""; // bu degisken hic kullanilmayacak

            var _ = ""; // kotu isimlendirme

            string gereksizIlkAtama = "a";
            gereksizIlkAtama = "b"; // deger degistirildigi icin ilk atama gereksiz.

            int buDegiskenSifirOlursaBolmeIslemiPatlar = 0;

            try
            {
                var riskliOperassyon = 1 / buDegiskenSifirOlursaBolmeIslemiPatlar; // riskli bolme islemi
            }
            catch (Exception ex)
            {
                // bos catch blogu...
            }

            var butunKullanicilar1 = db.Users.ToListAsync().Result; // yanlis async/await kullanimi - burada async islem uzerinde ".Result" cagirimi sebebiyle kod senkron calisir.
            var butunKullanicilar2 = db.Users.ToListAsync().Result; // ayni veriyi veritabanindan iki kere cekmek...

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == j)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            if (k == 5)
                            {
                                // neden...
                            }
                        }
                    }
                }
            }

            if (5 == 5) // her zaman true olacak bir condition
            {
                
            }

            while (true) 
            {
                break; // ???
            }

            int x = 123;
            int y = (int) x; // gereksiz type casting

            int i1 = 1;
            int i2 = 2;

            if (i1 == i2)
                return Results.Ok();
            else
                return Results.Ok(); // iki ihtimalde de ayni cevabi donmek...
        });

        app.Run();
    }
}
