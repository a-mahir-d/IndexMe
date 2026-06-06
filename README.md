# [IndexMe](https://indexme.ahmetmahirdemirelli.com)

* Linktree muadili bağlantı yönetim uygulaması.
* A Linktree clone.

---

## 🛠️ Tech Stack / Teknolojiler

### 🇹🇷 Türkçe
### Backend (.NET 10)
* **Clean Architecture:** Domain, Application, Infrastructure ve WebAPI katmanlarıyla gevşek bağlı ve sürdürülebilir mimari.
* **Rich Domain Driven Design (DDD):** İş kurallarını ve validasyonları kapsülleyen domain modelleri.
* **PostgreSQL (Neon):** Cloud-hosted ilişkisel veritabanı üzerinde optimize edilmiş kullanıcı ve bağlantı şemaları.
* **JWT Authentication:** Güvenli profil yönetimi ve dashboard erişimi için token tabanlı kimlik doğrulama.

### Frontend (Angular 21)
* **Angular Signals:** Reaktif profil düzenleme ve anlık önizleme senkronizasyonu için performanslı durum yönetimi.
* **Tailwind CSS v4:** Mobil öncelikli (responsive), modern ve minimalist kullanıcı açılış sayfaları.
* **Component-Driven Design:** Kod tekrarını önleyen, özelleştirilebilir link ve profil kartı bileşenleri.

### 🇺🇸 English
### Backend (.NET 10)
* **Clean Architecture:** Separated into Domain, Application, Infrastructure, and WebAPI layers for a loosely coupled, and maintainable codebase.
* **Rich Domain Driven Design (DDD):** Encapsulated business logic and validation constraints within domain entities.
* **PostgreSQL (Neon):** Optimized user profiles and links schema running on a cloud-hosted relational database.
* **JWT Authentication:** Token-based secure authentication management for dashboard access and profile modifications.

### Frontend (Angular 21)
* **Angular Signals:** High-performance reactive state management for real-time live preview and profile editing.
* **Tailwind CSS v4:** Mobile-first (responsive), modern, and minimalist custom user landing pages.
* **Component-Driven Design:** Highly reusable, custom link wrapper and profile presentation components.

---

## ⚙️ Core Logic / Temel Mantık

### 🇹🇷 Türkçe
* **Rich Domain Mantığı:** Domain varlıkları (Entity) geçerli bir durum olmadan oluşturulamaz; tüm iş kuralları ve URL doğrulama işlemleri doğrudan domain nesnesi içinde encapsulation prensibiyle işletilir.
* **Dinamik Profil Sayfaları:** Kullanıcıların eklediği bağlantılar, display order (sıralama) ve aktiflik durumlarına göre dinamik olarak render edilir.
* **Bulut Dağıtımı:** Sistem kaynaklarını optimize etmek ve tam uyumluluk sağlamak adına frontend mimarisi **Vercel** üzerinde, backend API servisleri ise **Render** üzerinde barındırılır.

### 🇺🇸 English
* **Rich Domain Encapsulation:** Domain entities enforce business rules and URL validation upon initialization, preventing invalid structural states across the system.
* **Dynamic Landing Pages:** User-defined custom social and web links are dynamically rendered on the public view based on display orders and active status.
* **Cloud Deployment:** For optimal resource scaling, the frontend application is hosted on **Vercel**, while the backend production API is deployed on **Render**.
