USE [Eticaret]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 10.01.2019 11:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[kategoriID] [int] IDENTITY(1,1) NOT NULL,
	[kategoriAd] [nvarchar](20) NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[kategoriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 10.01.2019 11:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[kullaniciID] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](50) NULL,
	[soyad] [nvarchar](50) NULL,
	[email] [varchar](100) NULL,
	[sifre] [nvarchar](50) NULL,
	[adres] [nvarchar](500) NOT NULL,
	[tc] [nvarchar](11) NOT NULL,
	[yetkiID] [int] NULL,
	[resimAd] [nvarchar](100) NULL,
	[durum] [bit] NULL,
	[telefonNo] [nvarchar](11) NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[kullaniciID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OzellikDeger]    Script Date: 10.01.2019 11:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OzellikDeger](
	[ozellikDegerID] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](50) NULL,
	[ozellikTipID] [int] NULL,
 CONSTRAINT [PK_OzellikDeger] PRIMARY KEY CLUSTERED 
(
	[ozellikDegerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OzellikTip]    Script Date: 10.01.2019 11:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OzellikTip](
	[ozellikTipID] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](50) NULL,
	[kategoriID] [int] NULL,
 CONSTRAINT [PK_OzellikTip] PRIMARY KEY CLUSTERED 
(
	[ozellikTipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Satis]    Script Date: 10.01.2019 11:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Satis](
	[satisID] [int] IDENTITY(1,1) NOT NULL,
	[kullaniciID] [int] NOT NULL,
	[satisTarihi] [datetime] NULL,
	[toplamTutar] [money] NULL,
	[siparisDurumID] [int] NULL,
 CONSTRAINT [PK_Satis] PRIMARY KEY CLUSTERED 
(
	[satisID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SatisDetayi]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SatisDetayi](
	[satisDetayID] [int] IDENTITY(1,1) NOT NULL,
	[urunID] [int] NULL,
	[satisID] [int] NULL,
	[fiyat] [money] NULL,
	[adet] [int] NULL,
 CONSTRAINT [PK_SatisDetayi] PRIMARY KEY CLUSTERED 
(
	[satisDetayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sepet]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sepet](
	[sepetID] [int] IDENTITY(1,1) NOT NULL,
	[kullaniciID] [int] NULL,
	[urunID] [int] NULL,
	[adet] [int] NULL,
	[fiyat] [money] NULL,
 CONSTRAINT [PK_Sepet] PRIMARY KEY CLUSTERED 
(
	[sepetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sifre]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sifre](
	[sifreID] [int] IDENTITY(1,1) NOT NULL,
	[kullaniciID] [int] NULL,
	[kod] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Sifre] PRIMARY KEY CLUSTERED 
(
	[sifreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiparisDurum]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiparisDurum](
	[siparisDurumID] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](50) NULL,
 CONSTRAINT [PK_SiparisDurum] PRIMARY KEY CLUSTERED 
(
	[siparisDurumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slider](
	[resimID] [int] IDENTITY(1,1) NOT NULL,
	[resimAd] [nvarchar](50) NULL,
	[aciklama] [nvarchar](50) NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[resimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urun]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urun](
	[urunID] [int] IDENTITY(1,1) NOT NULL,
	[urunAd] [nvarchar](50) NULL,
	[urunFiyat] [money] NULL,
	[urunAciklama] [nvarchar](500) NULL,
	[kategoriID] [int] NULL,
	[durum] [bit] NULL,
 CONSTRAINT [PK_Urun] PRIMARY KEY CLUSTERED 
(
	[urunID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunOzellik]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunOzellik](
	[urunID] [int] NOT NULL,
	[ozellikTipID] [int] NOT NULL,
	[ozellikDegerID] [int] NOT NULL,
 CONSTRAINT [PK_UrunOzellik] PRIMARY KEY CLUSTERED 
(
	[urunID] ASC,
	[ozellikTipID] ASC,
	[ozellikDegerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunResim]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunResim](
	[resimID] [int] IDENTITY(1,1) NOT NULL,
	[resimAd] [nvarchar](100) NULL,
	[urunID] [int] NULL,
 CONSTRAINT [PK_UrunResim] PRIMARY KEY CLUSTERED 
(
	[resimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Yetki]    Script Date: 10.01.2019 11:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Yetki](
	[yetkiID] [int] IDENTITY(1,1) NOT NULL,
	[yetkiAd] [nvarchar](15) NULL,
 CONSTRAINT [PK_Yetki] PRIMARY KEY CLUSTERED 
(
	[yetkiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Kategori] ON 

INSERT [dbo].[Kategori] ([kategoriID], [kategoriAd]) VALUES (1, N'KATEGORİSİZLER')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAd]) VALUES (1002, N'KIZ OYUNCAKLARI')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAd]) VALUES (1006, N'ERKEK OYUNCAKLARI')
INSERT [dbo].[Kategori] ([kategoriID], [kategoriAd]) VALUES (1007, N'1-4 YAŞ OYUNCAKLARI')
SET IDENTITY_INSERT [dbo].[Kategori] OFF
SET IDENTITY_INSERT [dbo].[Kullanici] ON 

INSERT [dbo].[Kullanici] ([kullaniciID], [ad], [soyad], [email], [sifre], [adres], [tc], [yetkiID], [resimAd], [durum], [telefonNo]) VALUES (1, N'FURKAN ', N'AKTAŞ', N'FURKANAKTAS487@GMAİL.COM', N'12345', N'VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK VELİBABA MAH. ÇAĞLAR SOKAK ', N'11111111111', 1, N'bc322bfc-ca40-4ef9-b3c6-0396d663bbf3.jpg', 1, N'05525205231')
INSERT [dbo].[Kullanici] ([kullaniciID], [ad], [soyad], [email], [sifre], [adres], [tc], [yetkiID], [resimAd], [durum], [telefonNo]) VALUES (2, N'YASİN', N'AKPINAR', N'YASİN@GMAİL.COM', N'12345', N'ŞİŞLİ MAHALLESİ', N'22222222222', 1, N'6f0d034e-04ac-42b5-9491-627cf7459184.jpg', 1, N'05525205231')
INSERT [dbo].[Kullanici] ([kullaniciID], [ad], [soyad], [email], [sifre], [adres], [tc], [yetkiID], [resimAd], [durum], [telefonNo]) VALUES (2028, N'BERKE', N'KORKMAZLAR', N'BERKE@GMAİL.COM', N'12345', N'MALTEPE İSTANBUL', N'12345678912', 2, N'556c24c2-9a72-4ec5-9af2-a301e374c431.jpg', 1, N'05545434343')
INSERT [dbo].[Kullanici] ([kullaniciID], [ad], [soyad], [email], [sifre], [adres], [tc], [yetkiID], [resimAd], [durum], [telefonNo]) VALUES (2029, N'EREN', N'FİDAN', N'EREN@GMAİL.COM', N'12345', N'BAĞCILAR İSTANBUL', N'12345678913', 2, N'default.jpg', 1, N'05467878987')
INSERT [dbo].[Kullanici] ([kullaniciID], [ad], [soyad], [email], [sifre], [adres], [tc], [yetkiID], [resimAd], [durum], [telefonNo]) VALUES (2030, N'AYŞE', N'NEŞELİ', N'AYSE@AYSE.COM', N'12345', N'DSFFSDFDSFDSSDF', N'12345678918', 2, N'10beb55f-1c56-43a5-b8ab-89929e23b630.jpg', 1, N'23123111231')
SET IDENTITY_INSERT [dbo].[Kullanici] OFF
SET IDENTITY_INSERT [dbo].[OzellikDeger] ON 

INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1, N'ALT ÖZELLİKSİZ', 1)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1022, N'UZAKTAN KUMANDALI', 3)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1023, N'LEGO', 3)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1024, N'BEZ BEBEK', 5)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1026, N'FUTBOL', 1007)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1028, N'AYICIK', 1008)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1029, N'YAPBOZ', 1010)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1030, N'BASKETBOL', 1007)
INSERT [dbo].[OzellikDeger] ([ozellikDegerID], [ad], [ozellikTipID]) VALUES (1031, N'MUTFAK SETİ', 1006)
SET IDENTITY_INSERT [dbo].[OzellikDeger] OFF
SET IDENTITY_INSERT [dbo].[OzellikTip] ON 

INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (1, N'ÖZELLİKSİZ', 1)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (3, N'ARABA', 1006)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (5, N'BEBEK', 1002)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (1006, N'OYUNCAK SETLERİ', 1002)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (1007, N'TOP', 1006)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (1008, N'PELUŞ HAYVANLAR', 1007)
INSERT [dbo].[OzellikTip] ([ozellikTipID], [ad], [kategoriID]) VALUES (1010, N'EĞİTİCİ OYUNLAR', 1007)
SET IDENTITY_INSERT [dbo].[OzellikTip] OFF
SET IDENTITY_INSERT [dbo].[SiparisDurum] ON 

INSERT [dbo].[SiparisDurum] ([siparisDurumID], [ad]) VALUES (1, N'HAZIRLANIYOR')
INSERT [dbo].[SiparisDurum] ([siparisDurumID], [ad]) VALUES (2, N'KARGOYA VERİLDİ')
INSERT [dbo].[SiparisDurum] ([siparisDurumID], [ad]) VALUES (3, N'TESLİM EDİLDİ')
INSERT [dbo].[SiparisDurum] ([siparisDurumID], [ad]) VALUES (4, N'İPTAL EDİLDİ')
SET IDENTITY_INSERT [dbo].[SiparisDurum] OFF
SET IDENTITY_INSERT [dbo].[Slider] ON 

INSERT [dbo].[Slider] ([resimID], [resimAd], [aciklama]) VALUES (1, N'782849ee-b0ce-439e-a37a-d222f9aaa5bd.jpg', N'HEP BİRLİKTE BERABER İLERLEYECEĞİZ')
INSERT [dbo].[Slider] ([resimID], [resimAd], [aciklama]) VALUES (2, N'd3b3f3f0-95b6-4219-a680-031ae009b30c.jpg', N'GELECEĞE DOĞRU EMİN ADIMLARLA GİDECEĞİZ')
INSERT [dbo].[Slider] ([resimID], [resimAd], [aciklama]) VALUES (3, N'default.jpg', N'DEFAULT ACİKLAMA')
SET IDENTITY_INSERT [dbo].[Slider] OFF
SET IDENTITY_INSERT [dbo].[Urun] ON 

INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (19, N'ÇİLEK KIZ 60CM', 45.0000, N'45 CM BOYUNDA ŞARKI SÖYLEYEN ÇİLEK KIZ BEZ BEBEK

GÖĞSÜNE BASILDIĞINDA TÜRKÇE ÇİLEK KIZ ŞARKISI SÖYLER

HAYDİ KIZLAR ÇİLEK KIZ İLE BİRLİKTE ŞARKI SÖYLEYELİM.

YAŞ GRUBU : 3+ YAŞ
KUTU ÖLÇÜLERİ : 45 X 19 X 14 CM.', 1002, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (20, N'SARI KIZ VE ARKADAŞLARI', 25.0000, N'ÜRÜNLERİMİZ ADET BAŞI SATILMAKTADIR. ADET FİYATI 25 TL''DİR. KARGO ÜCRETİ ŞİRKETİMİZ TARAFINDAN KARŞILANMAKTADIR.', 1002, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (21, N'3 PARÇA MUTFAK SETİ IŞIKLI', 92.0000, N'BU OYUNCAK SETİ İLE ÇOCUKLAR EĞLENCELİ VAKİT GEÇİRECEKLER VE
HAYAL DÜNYALARINI GELİŞTİRECEKLER.

BOL AKSESUARLI BU MUTFAK SETİNİN KAPI VE ÇEKMECELERİ AÇILABİLİR.

 

ÜRÜN UZUNLUĞU TAKRİBİ 35 CM DİR.



ÜRÜN 2 ADET AAA 1,5V PİL İLE ÇALIŞIR.(PİLLER DAHİL DEĞİLDİR.)
 ', 1002, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (22, N'CARS 3 KARAKTERİ JACKSON STORM KUMANDALI ARABA', 180.0000, N'ARABALAR 3 (CARS 3) FİLMİNİN SEVİLEN KARAKTERLERİNDEN JACKSON STORM''UN ŞİMŞEK MCQUEEN İLE REKABETİNE SİZ DE ORTAK OLABİLİRSİNİZ. UZAKTAN KUMANDALI YARIŞ ARABASI İLE PİSTTEKİ YERİNİZİ ALIN VE HEYACANI SİZ DE YAŞAYIN. TURBO ÖZELLİĞİYLE ARABANIZIN HIZINI DAHA DA ARTTIRABİLİR, GELİŞMİŞ MANEVRA KABİLİYETİ İLE EN KESKİN DÖNÜŞLERİ KOLAYCA YAPABİLİRSİNİZ. ', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (23, N'GAMESTAR KUM KASIRGASI UZAKTAN KUMANDALI ', 160.0000, N'ŞARJLI BUGGY ARABA
ÜRÜN ÖZELLİKLERİ: 
FUL FONKSİYON KUMANDA
BAĞIMSIZ 4 TEKERLEKTEN SÜSPANSİYON SİSTEMİ
KIRILMAZ SAĞLAM GÖVDE VE AKSESUARLAR
SÜPER YÜKSEK HIZLI MOTOR TEKNOLOJİSİ
İLERİ, GERİ, İLERİ SAĞ, İLERİ SOL, GERİ SAĞ, GERİ SOL, VE DURMA...
6 VOLT ŞARJ EDİLEBİLİR ARAÇ PİLİ KUTUYA DAHİLDİR. ', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (24, N'NİKE FUTBOL TOPU PTCH TRAİN', 55.0000, N'TOPUN BOYUTU: 5 NO (12 YAŞ VE ÜZERİ)
YÜZEYİ: POLİÜRETAN DERİ, LATEKS İÇ LASTİK
AĞIRLIĞI: 426 GR
KULLANIM ALANLARI: YUMUŞAK ZEMİNDE KULLANIMA UYGUN
FEDERASYON ONAYI: YOK
DİĞER ÖZELLİKLERİ: MAKİNE DİKİŞLİ, 12 PARÇA FUTBOL TOPU', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (25, N'ADİDAS FİNALE14RM MİNİ FUTBOL TOPU', 80.0000, N'GARANTİ SÜRESİ 1 YILDIR. ÖZEL OLARAK ÜRETİLEN BU TOPLA KENDİNİZİ GERÇEK BİR FUTBOLCU GİBİ HİSSEDECEKSİNİZ.', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (26, N'SPALDİNG BASKETBOL TOPU ', 105.0000, N' SPALDİNG TARAFINDAN EUROLEAGUE İÇİN ÖZEL OLARAK GELİŞTİRİLEN KAUÇUK MALZEMEDEN ÜRETİLMİŞTİR.
* TÜM LOGOLAR KABARTMA BASKILIDIR.
* EUROLEAGUE İÇİN ÖZEL TASARLANMIŞ 8 PANELE SAHİPTİR.
* DIŞ VE İÇ MEKAN OYUNLARI İÇİN UYGUNDUR
* SPALDİNG NBA, WNBA, D-LEAGUE VE EUROLEAGUE RESMİ SPONSORUDUR.
* NO:5 BOYUTUNDADIR.', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (27, N'BUİLD PLAY YAP BOZ LEGO ARABA UCAK MOTOSİKLET', 75.0000, N'1 KUTUDA 3 OYUNCAK MODELİ. ZİHİNSEL VE FİZİKSEL GELİŞİM, PROBLEM ÇÖZME YETİSİ, HAYAL GÜCÜ, EL VE GÖZ KOORDİNASYONU, KÜÇÜK MOTOR KAS GELİŞİMİ İÇİN FAYDALIDIR.', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (28, N'LEGO CİTY 60239 POLİS DEVRİYE ARABASI', 60.0000, N'
LEGO CİTY 60239 POLİS DEVRİYE ARABASI

POLİS DEVRİYE ARABASI OYUN SETİYLE GÜNÜN KAHRAMANI OL!

POLİS DEVRİYE ARABASI’NIN GÜÇLÜ MOTORUNU ÇALIŞTIR VE ŞEHRİ KORU! HEY, POLİS MEMURUNUN TRAFİĞİ YÖNLENDİRMEDE YARDIMA İHTİYACI VAR GİBİ GÖRÜNÜYOR. TRAFİK KONİLERİNİ YERLEŞTİR VE LEGO® CİTY POLİS GÜCÜNÜN BİR KAHRAMANI OL!', 1006, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (29, N'100 CM SEVİMLİ OYUNCAK PELUŞ AYI ', 110.0000, N'SEVİMLİ PELUŞ AYICIK, BEYAZ RENKTE,O RTALAMA 2.5 - 3 KG AĞIRLINDA, 100 CM BOYUNDA VE 55 CM ENİNDEDİR.', 1007, 1)
INSERT [dbo].[Urun] ([urunID], [urunAd], [urunFiyat], [urunAciklama], [kategoriID], [durum]) VALUES (30, N'AHŞAP PUZZLE AHŞAP YAPBOZ 9 PARÇALIK', 15.0000, N'1-4 YAŞ İÇİN AHŞAP PUZZLE MİNİ BOY- 9 PARÇALI- ONLARCA FARKLI ÇEŞİT AYNI GÜN HIZLI KARGO DİKKAT : DESENLER KARIŞIK GÖNDERİLECEKTİR, LÜTFEN DESEN SEÇİP MESAJ ATMAYINIZ, SEÇME İMKANIMIZ YOKTUR, BİRDEN FAZLA ALIMLARDA HER PUZZLE FARKLI MODEL GÖNDERİLECEKTİR.', 1007, 1)
SET IDENTITY_INSERT [dbo].[Urun] OFF
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (19, 5, 1024)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (20, 5, 1024)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (21, 1006, 1031)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (22, 3, 1022)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (23, 3, 1022)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (24, 1007, 1026)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (25, 1007, 1026)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (26, 1007, 1030)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (27, 3, 1023)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (28, 3, 1023)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (29, 1008, 1028)
INSERT [dbo].[UrunOzellik] ([urunID], [ozellikTipID], [ozellikDegerID]) VALUES (30, 1010, 1029)
SET IDENTITY_INSERT [dbo].[UrunResim] ON 

INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1027, N'd17756ea-f152-4c86-a033-9a04b1ad03ba.jpg', 19)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1028, N'a2fdbdd9-a04d-4dbf-a938-e521cf7ef84d.jpg', 19)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1029, N'77f47b59-81ca-4b54-9cbc-48bf0e349858.jpg', 19)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1030, N'17f44291-f2c6-476d-ab00-680be578b46a.jpg', 20)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1031, N'ed6492e2-ee78-4306-a46d-914cf7f63ede.jpg', 20)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1032, N'3fb8e009-fa6c-438e-9f34-4acd30b7034b.jpg', 20)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1033, N'f86ff937-3c61-4e9d-a177-d41c217f511a.jpg', 21)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1034, N'0f3c20d9-1251-4954-8aa9-fb84b935b5a2.jpg', 21)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1035, N'default.png', 21)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1036, N'd336d414-57e4-447e-9485-66082dba8b91.jpg', 22)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1037, N'd2212c91-7816-463f-80fc-9f9c588c0db9.jpg', 22)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1038, N'default.png', 22)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1039, N'5fa0f609-82b8-4ce5-90ac-b9be8432de7e.jpg', 23)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1040, N'ed54833f-e932-4f01-828a-b3674cd08ce2.jpg', 23)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1041, N'9375e450-f234-4fd6-a6e1-d069b3e32671.jpg', 23)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1042, N'4f45e444-42d9-4362-b6e1-9d0d0e6beffd.jpg', 24)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1043, N'7200bb83-45aa-41e6-ad69-afae280690c3.jpg', 24)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1044, N'13606912-2895-4cec-be33-2912b51e43d6.jpg', 24)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1045, N'34c569d4-6536-4fd2-8f96-47c925cc8916.jpg', 25)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1046, N'default.png', 25)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1047, N'default.png', 25)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1048, N'd9f1227e-827a-4669-81a9-bf130a176327.jpg', 26)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1049, N'default.png', 26)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1050, N'default.png', 26)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1051, N'06790503-ba47-42b5-8a5a-3975a273eedb.jpg', 27)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1052, N'9573d6fa-5d61-44c3-8e5d-3debd48a751a.jpg', 27)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1053, N'8668f294-92c6-41d6-ab50-5dd815b7eb9f.jpg', 27)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1054, N'ca134505-8d7f-44f5-9fba-4376b101c786.jpg', 28)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1055, N'43e5c05f-b120-482d-8892-15ac019fa2c9.jpg', 28)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1056, N'f83ade9d-6484-4dee-bf2d-ad03d827f141.jpg', 28)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1057, N'4f6b8850-bdb7-49ad-bd34-3a4e03bc4038.jpg', 29)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1058, N'5fc78667-1fa6-4fd8-88b7-556001679fdc.jpg', 29)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1059, N'7e334c55-ec93-47d8-aae8-130697b943aa.jpg', 29)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1060, N'ceb6bbdd-a0d6-46a1-9d97-ed4a3d6bccdd.jpg', 30)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1061, N'94e03630-a78b-4006-a473-03d084b6b4d8.jpg', 30)
INSERT [dbo].[UrunResim] ([resimID], [resimAd], [urunID]) VALUES (1062, N'6647c322-5c02-438c-af0c-714a508243d2.jpg', 30)
SET IDENTITY_INSERT [dbo].[UrunResim] OFF
SET IDENTITY_INSERT [dbo].[Yetki] ON 

INSERT [dbo].[Yetki] ([yetkiID], [yetkiAd]) VALUES (1, N'ADMİN')
INSERT [dbo].[Yetki] ([yetkiID], [yetkiAd]) VALUES (2, N'MÜŞTERİ')
SET IDENTITY_INSERT [dbo].[Yetki] OFF
ALTER TABLE [dbo].[Satis] ADD  CONSTRAINT [DF_Satis_satisTarihi]  DEFAULT (getdate()) FOR [satisTarihi]
GO
ALTER TABLE [dbo].[Kullanici]  WITH CHECK ADD  CONSTRAINT [FK_Kullanici_Yetki] FOREIGN KEY([yetkiID])
REFERENCES [dbo].[Yetki] ([yetkiID])
GO
ALTER TABLE [dbo].[Kullanici] CHECK CONSTRAINT [FK_Kullanici_Yetki]
GO
ALTER TABLE [dbo].[OzellikDeger]  WITH CHECK ADD  CONSTRAINT [FK_OzellikDeger_OzellikTip] FOREIGN KEY([ozellikTipID])
REFERENCES [dbo].[OzellikTip] ([ozellikTipID])
GO
ALTER TABLE [dbo].[OzellikDeger] CHECK CONSTRAINT [FK_OzellikDeger_OzellikTip]
GO
ALTER TABLE [dbo].[OzellikTip]  WITH CHECK ADD  CONSTRAINT [FK_OzellikTip_Kategori] FOREIGN KEY([kategoriID])
REFERENCES [dbo].[Kategori] ([kategoriID])
GO
ALTER TABLE [dbo].[OzellikTip] CHECK CONSTRAINT [FK_OzellikTip_Kategori]
GO
ALTER TABLE [dbo].[Satis]  WITH CHECK ADD  CONSTRAINT [FK_Satis_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
GO
ALTER TABLE [dbo].[Satis] CHECK CONSTRAINT [FK_Satis_Kullanici]
GO
ALTER TABLE [dbo].[Satis]  WITH CHECK ADD  CONSTRAINT [FK_Satis_SiparisDurum] FOREIGN KEY([siparisDurumID])
REFERENCES [dbo].[SiparisDurum] ([siparisDurumID])
GO
ALTER TABLE [dbo].[Satis] CHECK CONSTRAINT [FK_Satis_SiparisDurum]
GO
ALTER TABLE [dbo].[SatisDetayi]  WITH CHECK ADD  CONSTRAINT [FK_SatisDetayi_Satis] FOREIGN KEY([satisID])
REFERENCES [dbo].[Satis] ([satisID])
GO
ALTER TABLE [dbo].[SatisDetayi] CHECK CONSTRAINT [FK_SatisDetayi_Satis]
GO
ALTER TABLE [dbo].[SatisDetayi]  WITH CHECK ADD  CONSTRAINT [FK_SatisDetayi_Urun] FOREIGN KEY([urunID])
REFERENCES [dbo].[Urun] ([urunID])
GO
ALTER TABLE [dbo].[SatisDetayi] CHECK CONSTRAINT [FK_SatisDetayi_Urun]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Kullanici]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Urun] FOREIGN KEY([urunID])
REFERENCES [dbo].[Urun] ([urunID])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Urun]
GO
ALTER TABLE [dbo].[Sifre]  WITH CHECK ADD  CONSTRAINT [FK_Sifre_Kullanici] FOREIGN KEY([kullaniciID])
REFERENCES [dbo].[Kullanici] ([kullaniciID])
GO
ALTER TABLE [dbo].[Sifre] CHECK CONSTRAINT [FK_Sifre_Kullanici]
GO
ALTER TABLE [dbo].[Urun]  WITH CHECK ADD  CONSTRAINT [FK_Urun_Kategori] FOREIGN KEY([kategoriID])
REFERENCES [dbo].[Kategori] ([kategoriID])
GO
ALTER TABLE [dbo].[Urun] CHECK CONSTRAINT [FK_Urun_Kategori]
GO
ALTER TABLE [dbo].[UrunOzellik]  WITH CHECK ADD  CONSTRAINT [FK_UrunOzellik_OzellikDeger] FOREIGN KEY([ozellikDegerID])
REFERENCES [dbo].[OzellikDeger] ([ozellikDegerID])
GO
ALTER TABLE [dbo].[UrunOzellik] CHECK CONSTRAINT [FK_UrunOzellik_OzellikDeger]
GO
ALTER TABLE [dbo].[UrunOzellik]  WITH CHECK ADD  CONSTRAINT [FK_UrunOzellik_OzellikTip] FOREIGN KEY([ozellikTipID])
REFERENCES [dbo].[OzellikTip] ([ozellikTipID])
GO
ALTER TABLE [dbo].[UrunOzellik] CHECK CONSTRAINT [FK_UrunOzellik_OzellikTip]
GO
ALTER TABLE [dbo].[UrunOzellik]  WITH CHECK ADD  CONSTRAINT [FK_UrunOzellik_Urun] FOREIGN KEY([urunID])
REFERENCES [dbo].[Urun] ([urunID])
GO
ALTER TABLE [dbo].[UrunOzellik] CHECK CONSTRAINT [FK_UrunOzellik_Urun]
GO
ALTER TABLE [dbo].[UrunResim]  WITH CHECK ADD  CONSTRAINT [FK_UrunResim_Urun] FOREIGN KEY([urunID])
REFERENCES [dbo].[Urun] ([urunID])
GO
ALTER TABLE [dbo].[UrunResim] CHECK CONSTRAINT [FK_UrunResim_Urun]
GO
