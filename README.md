# 🛒 EcommerceAPI

EcommerceAPI adalah **RESTful API berbasis ASP.NET Core 8** dengan **Entity Framework Core** yang mensimulasikan sistem e-commerce sederhana.  
API ini mendukung manajemen user, produk, cart, order, dan simulasi pembayaran.
Project ini merupakan **assignment setelah mengikuti refresment class belajar ASP.NET selama 2 minggu di bootcamp [dibimbing.id](https://dibimbing.id)**.  


---

## ✨ Fitur Utama

### 🔐 User Management
- Register user baru (role: `admin` / `customer`)
- Login & mendapatkan JWT token
- Melihat & update profil user

### 📦 Product Management
- Tambah produk (admin)
- Update produk (admin)
- Hapus produk (admin)
- List produk (public)
- Detail produk (public)

### 🛒 Cart & Checkout
- Tambah produk ke cart (customer)
- Update jumlah item di cart
- Hapus item dari cart
- Lihat isi cart
- Checkout → membuat order

### 📑 Order Management
- Customer: lihat riwayat order
- Admin: lihat semua order
- Update status order (`pending → paid → shipped → completed`)

### 💳 Payment Simulation
- Endpoint untuk simulasi pembayaran sukses/gagal

---

## 🏗️ Tech Stack

- [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [JWT Authentication](https://jwt.io/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) untuk dokumentasi API
- [Docker](https://www.docker.com/) untuk containerization

---

## ⚙️ Instalasi & Menjalankan di Lokal

### 1. Clone Repository
```bash
git clone https://github.com/<username>/EcommerceAPI.git
cd EcommerceAPI


### 2. Setup Database
```Pastikan SQL Server sudah jalan (bisa via Docker):
docker run -d -p 1433:1433 --name ecommerce-db \
  -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" \
  mcr.microsoft.com/mssql/server:2022-latest


### 3. Update Connection String
"ConnectionStrings": {
  "EcommerceDB": "Server=localhost,1433;Database=EcommerceDB;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;"
}


###4. Jalankan Migration
dotnet ef database update


### 5. RUN API
dotnet run
API akan jalan di: [http://localhost:5000/swagger](http://localhost:5114/swagger/index.html)


### 🧪 Postman Collection
ada di folder docs
