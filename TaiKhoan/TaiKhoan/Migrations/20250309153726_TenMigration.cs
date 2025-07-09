using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAn2811_.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BAO_CAO",
                columns: table => new
                {
                    MA_BAO_CAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN_BAO_CAO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SO_LUONG_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    TONG_DANH_THU = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    NGAY_XUAT_BAO_CAO = table.Column<DateOnly>(type: "date", nullable: true),
                    GHICHU = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BAO_CAO__3334D179FB362085", x => x.MA_BAO_CAO);
                });

            migrationBuilder.CreateTable(
                name: "DANH_MUC",
                columns: table => new
                {
                    Ma_Danh_Muc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Danh_Muc = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DANH_MUC__ACC6D9BB36F3E41C", x => x.Ma_Danh_Muc);
                });

            migrationBuilder.CreateTable(
                name: "KHUYEN_MAI",
                columns: table => new
                {
                    MA_KHUYEN_MAI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN_KHUYEM_MAI = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MO_TA_KHUYEN_MAI = table.Column<string>(type: "text", nullable: true),
                    NGAY_AP_DUNG = table.Column<DateOnly>(type: "date", nullable: true),
                    NGAY_KET_THUC = table.Column<DateOnly>(type: "date", nullable: true),
                    SO_LUONG_KHUYEN_MAI = table.Column<int>(type: "int", nullable: true),
                    DIEU_KIEN = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHUYEN_M__91682CA17804E32B", x => x.MA_KHUYEN_MAI);
                });

            migrationBuilder.CreateTable(
                name: "NHAP_KHO",
                columns: table => new
                {
                    MA_NHAP_KHO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DON_VI_NHAP_HANG = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NGAY_NHAP = table.Column<DateOnly>(type: "date", nullable: true),
                    GHI_CHI = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NHAP_KHO__6874D7D8A9A7E5C9", x => x.MA_NHAP_KHO);
                });

            migrationBuilder.CreateTable(
                name: "PHUONG_THUC_GD",
                columns: table => new
                {
                    MA_PHUONG_THUC_GD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN_PHUONG_THUC_GD = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PHUONG_T__DD4C335A939AEC12", x => x.MA_PHUONG_THUC_GD);
                });

            migrationBuilder.CreateTable(
                name: "PHUONG_THUC_THANH_TOAN",
                columns: table => new
                {
                    MA_PHUONG_THUC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN_PHUONG_THUC = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PHUONG_T__31662E8170FAF5F6", x => x.MA_PHUONG_THUC);
                });

            migrationBuilder.CreateTable(
                name: "TAI_kHOAN",
                columns: table => new
                {
                    Ma_TK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TenDangNhap = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TAI_kHOA__2E62FB7C5190AE69", x => x.Ma_TK);
                });

            migrationBuilder.CreateTable(
                name: "XUAT_KHO",
                columns: table => new
                {
                    MA_XUAT_KHO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DON_VI_XUAT_KHO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NGAY_XUAT = table.Column<DateOnly>(type: "date", nullable: true),
                    GHI_CHI = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__XUAT_KHO__36F544112A26A70F", x => x.MA_XUAT_KHO);
                });

            migrationBuilder.CreateTable(
                name: "HANG_SAN_XUAT",
                columns: table => new
                {
                    Ma_Hang_San_XuaT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_Danh_Muc = table.Column<int>(type: "int", nullable: true),
                    Ten_Hang = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HANG_SAN__891CE634389FE8E6", x => x.Ma_Hang_San_XuaT);
                    table.ForeignKey(
                        name: "FK__HANG_SAN___Ma_Da__3B75D760",
                        column: x => x.Ma_Danh_Muc,
                        principalTable: "DANH_MUC",
                        principalColumn: "Ma_Danh_Muc");
                });

            migrationBuilder.CreateTable(
                name: "DON_HANG",
                columns: table => new
                {
                    MA_DON_HANG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NGAY_DAT_HANG = table.Column<DateOnly>(type: "date", nullable: true),
                    TRANG_THAI_DON_HANG = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    TONG_DON_HANG = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    MA_PHUONG_THUC = table.Column<int>(type: "int", nullable: true),
                    MA_PHUONG_THUC_GH = table.Column<int>(type: "int", nullable: true),
                    MA_THEO_DOI = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    GHICHU = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DON_HANG__A7174667F23B81F3", x => x.MA_DON_HANG);
                    table.ForeignKey(
                        name: "FK__DON_HANG__MA_PHU__571DF1D5",
                        column: x => x.MA_PHUONG_THUC,
                        principalTable: "PHUONG_THUC_THANH_TOAN",
                        principalColumn: "MA_PHUONG_THUC");
                    table.ForeignKey(
                        name: "FK__DON_HANG__MA_PHU__5812160E",
                        column: x => x.MA_PHUONG_THUC_GH,
                        principalTable: "PHUONG_THUC_GD",
                        principalColumn: "MA_PHUONG_THUC_GD");
                });

            migrationBuilder.CreateTable(
                name: "DANH_SACH_YEU_THICH",
                columns: table => new
                {
                    MA_YEU_THICH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_TK = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DANH_SAC__99D274B48230816D", x => x.MA_YEU_THICH);
                    table.ForeignKey(
                        name: "FK__DANH_SACH__Ma_TK__440B1D61",
                        column: x => x.Ma_TK,
                        principalTable: "TAI_kHOAN",
                        principalColumn: "Ma_TK");
                });

            migrationBuilder.CreateTable(
                name: "DIA_CHI_GIAO_HANG",
                columns: table => new
                {
                    MA_DIA_CHI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_TK = table.Column<int>(type: "int", nullable: true),
                    HO_TEN = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    SO_DIEN_THOAI = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TINH = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    HUYEN = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    XA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    CHI_TIET = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DIA_CHI___3BCAD161B4A1710E", x => x.MA_DIA_CHI);
                    table.ForeignKey(
                        name: "FK__DIA_CHI_G__Ma_TK__5441852A",
                        column: x => x.Ma_TK,
                        principalTable: "TAI_kHOAN",
                        principalColumn: "Ma_TK");
                });

            migrationBuilder.CreateTable(
                name: "GIO_HANG",
                columns: table => new
                {
                    MA_GIO_HANG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_TK = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG_SAN_PHAM = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GIO_HANG__16B948AB72AFE0D1", x => x.MA_GIO_HANG);
                    table.ForeignKey(
                        name: "FK__GIO_HANG__Ma_TK__49C3F6B7",
                        column: x => x.Ma_TK,
                        principalTable: "TAI_kHOAN",
                        principalColumn: "Ma_TK");
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAM",
                columns: table => new
                {
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN_SAN_PHAM = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MO_TA = table.Column<string>(type: "text", nullable: true),
                    Ma_Hang_San_Xuat = table.Column<int>(type: "int", nullable: true),
                    ANH = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SAN_PHAM__AEAADD69359BD171", x => x.MA_SAN_PHAM);
                    table.ForeignKey(
                        name: "FK__SAN_PHAM__Ma_Han__3E52440B",
                        column: x => x.Ma_Hang_San_Xuat,
                        principalTable: "HANG_SAN_XUAT",
                        principalColumn: "Ma_Hang_San_XuaT");
                });

            migrationBuilder.CreateTable(
                name: "THANH_TOAN",
                columns: table => new
                {
                    MA_THANH_TOAN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_DON_HANG = table.Column<int>(type: "int", nullable: true),
                    NGAY_THANH_TOAN = table.Column<DateOnly>(type: "date", nullable: true),
                    MA_PHUONG_THUC = table.Column<int>(type: "int", nullable: true),
                    MA_PHUONG_THUC_GH = table.Column<int>(type: "int", nullable: true),
                    SO_TIEN_CAN_THANH_TOAN = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    TRANG_THAI = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__THANH_TO__D82DF910AE59EE27", x => x.MA_THANH_TOAN);
                    table.ForeignKey(
                        name: "FK__THANH_TOA__MA_DO__5EBF139D",
                        column: x => x.MA_DON_HANG,
                        principalTable: "DON_HANG",
                        principalColumn: "MA_DON_HANG");
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_DON_HANG",
                columns: table => new
                {
                    MA_CHI_TIET_DON_HANG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_DON_HANG = table.Column<int>(type: "int", nullable: true),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG_MUA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHI_TIET__8280DDB8B2E23CE1", x => x.MA_CHI_TIET_DON_HANG);
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_DO__5AEE82B9",
                        column: x => x.MA_DON_HANG,
                        principalTable: "DON_HANG",
                        principalColumn: "MA_DON_HANG");
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_SA__5BE2A6F2",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_GIO_HANG",
                columns: table => new
                {
                    MA_CHI_TIET_GIO_HANG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    MA_GIO_HANG = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHI_TIET__D8C4F0E7A53A8B5D", x => x.MA_CHI_TIET_GIO_HANG);
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_GI__4D94879B",
                        column: x => x.MA_GIO_HANG,
                        principalTable: "GIO_HANG",
                        principalColumn: "MA_GIO_HANG");
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_SA__4CA06362",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_KHUYEN_MAI",
                columns: table => new
                {
                    MA_CHI_TIET_KHUYEN_MAI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    MA_KHUYEN_MAI = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHI_TIET__D6DAF54F1990275C", x => x.MA_CHI_TIET_KHUYEN_MAI);
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_KH__6383C8BA",
                        column: x => x.MA_KHUYEN_MAI,
                        principalTable: "KHUYEN_MAI",
                        principalColumn: "MA_KHUYEN_MAI");
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_SA__6477ECF3",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_SAN_PHAM",
                columns: table => new
                {
                    MA_CHI_TIET = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG = table.Column<int>(type: "int", nullable: true),
                    GIA = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    LOAI_SAN_PHAM = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    THUOC_TINH = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHI_TIET__A5990A2201FC270D", x => x.MA_CHI_TIET);
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_SA__412EB0B6",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_XUAT_KHO",
                columns: table => new
                {
                    MA_CHI_TIET_XUAT_KHO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_XUAT_KHO = table.Column<int>(type: "int", nullable: true),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG_XUAT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHI_TIET__E1A792A5E732B09A", x => x.MA_CHI_TIET_XUAT_KHO);
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_SA__72C60C4A",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                    table.ForeignKey(
                        name: "FK__CHI_TIET___MA_XU__71D1E811",
                        column: x => x.MA_XUAT_KHO,
                        principalTable: "XUAT_KHO",
                        principalColumn: "MA_XUAT_KHO");
                });

            migrationBuilder.CreateTable(
                name: "NHAP_KHO_CHI_TIET",
                columns: table => new
                {
                    NHAP_CHI_TIET = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_NHAP_KHO = table.Column<int>(type: "int", nullable: true),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NHAP_KHO__AE1443ED0B5EB7D4", x => x.NHAP_CHI_TIET);
                    table.ForeignKey(
                        name: "FK__NHAP_KHO___MA_NH__6C190EBB",
                        column: x => x.MA_NHAP_KHO,
                        principalTable: "NHAP_KHO",
                        principalColumn: "MA_NHAP_KHO");
                    table.ForeignKey(
                        name: "FK__NHAP_KHO___MA_SA__6D0D32F4",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "QUAN_LY_TON_KHO",
                columns: table => new
                {
                    MA_TON_KHO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG_TON = table.Column<int>(type: "int", nullable: true),
                    NGAY_CAP_NHAT = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QUAN_LY___5CC9326D89AC93F8", x => x.MA_TON_KHO);
                    table.ForeignKey(
                        name: "FK__QUAN_LY_T__MA_SA__6754599E",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAM_YEU_THICH",
                columns: table => new
                {
                    MA_CHI_TIET_YEU_THICH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MA_SAN_PHAM = table.Column<int>(type: "int", nullable: true),
                    SO_LUONG = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SAN_PHAM__CC600104CF93BB80", x => x.MA_CHI_TIET_YEU_THICH);
                    table.ForeignKey(
                        name: "FK__SAN_PHAM___MA_SA__46E78A0C",
                        column: x => x.MA_SAN_PHAM,
                        principalTable: "SAN_PHAM",
                        principalColumn: "MA_SAN_PHAM");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_DON_HANG_MA_DON_HANG",
                table: "CHI_TIET_DON_HANG",
                column: "MA_DON_HANG");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_DON_HANG_MA_SAN_PHAM",
                table: "CHI_TIET_DON_HANG",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_GIO_HANG_MA_GIO_HANG",
                table: "CHI_TIET_GIO_HANG",
                column: "MA_GIO_HANG");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_GIO_HANG_MA_SAN_PHAM",
                table: "CHI_TIET_GIO_HANG",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_KHUYEN_MAI_MA_KHUYEN_MAI",
                table: "CHI_TIET_KHUYEN_MAI",
                column: "MA_KHUYEN_MAI");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_KHUYEN_MAI_MA_SAN_PHAM",
                table: "CHI_TIET_KHUYEN_MAI",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_SAN_PHAM_MA_SAN_PHAM",
                table: "CHI_TIET_SAN_PHAM",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_XUAT_KHO_MA_SAN_PHAM",
                table: "CHI_TIET_XUAT_KHO",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_XUAT_KHO_MA_XUAT_KHO",
                table: "CHI_TIET_XUAT_KHO",
                column: "MA_XUAT_KHO");

            migrationBuilder.CreateIndex(
                name: "IX_DANH_SACH_YEU_THICH_Ma_TK",
                table: "DANH_SACH_YEU_THICH",
                column: "Ma_TK");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_CHI_GIAO_HANG_Ma_TK",
                table: "DIA_CHI_GIAO_HANG",
                column: "Ma_TK");

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_MA_PHUONG_THUC",
                table: "DON_HANG",
                column: "MA_PHUONG_THUC");

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_MA_PHUONG_THUC_GH",
                table: "DON_HANG",
                column: "MA_PHUONG_THUC_GH");

            migrationBuilder.CreateIndex(
                name: "IX_GIO_HANG_Ma_TK",
                table: "GIO_HANG",
                column: "Ma_TK");

            migrationBuilder.CreateIndex(
                name: "IX_HANG_SAN_XUAT_Ma_Danh_Muc",
                table: "HANG_SAN_XUAT",
                column: "Ma_Danh_Muc");

            migrationBuilder.CreateIndex(
                name: "IX_NHAP_KHO_CHI_TIET_MA_NHAP_KHO",
                table: "NHAP_KHO_CHI_TIET",
                column: "MA_NHAP_KHO");

            migrationBuilder.CreateIndex(
                name: "IX_NHAP_KHO_CHI_TIET_MA_SAN_PHAM",
                table: "NHAP_KHO_CHI_TIET",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_QUAN_LY_TON_KHO_MA_SAN_PHAM",
                table: "QUAN_LY_TON_KHO",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IDX_SAN_PHAM_ANH",
                table: "SAN_PHAM",
                column: "ANH");

            migrationBuilder.CreateIndex(
                name: "IDX_SAN_PHAM_TEN_SAN_PHAM",
                table: "SAN_PHAM",
                column: "TEN_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_Ma_Hang_San_Xuat",
                table: "SAN_PHAM",
                column: "Ma_Hang_San_Xuat");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_YEU_THICH_MA_SAN_PHAM",
                table: "SAN_PHAM_YEU_THICH",
                column: "MA_SAN_PHAM");

            migrationBuilder.CreateIndex(
                name: "IDX_TAI_KHOAN_MK",
                table: "TAI_kHOAN",
                column: "MatKhau");

            migrationBuilder.CreateIndex(
                name: "IDX_TAI_KHOAN_TEN",
                table: "TAI_kHOAN",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_THANH_TOAN_MA_DON_HANG",
                table: "THANH_TOAN",
                column: "MA_DON_HANG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAO_CAO");

            migrationBuilder.DropTable(
                name: "CHI_TIET_DON_HANG");

            migrationBuilder.DropTable(
                name: "CHI_TIET_GIO_HANG");

            migrationBuilder.DropTable(
                name: "CHI_TIET_KHUYEN_MAI");

            migrationBuilder.DropTable(
                name: "CHI_TIET_SAN_PHAM");

            migrationBuilder.DropTable(
                name: "CHI_TIET_XUAT_KHO");

            migrationBuilder.DropTable(
                name: "DANH_SACH_YEU_THICH");

            migrationBuilder.DropTable(
                name: "DIA_CHI_GIAO_HANG");

            migrationBuilder.DropTable(
                name: "NHAP_KHO_CHI_TIET");

            migrationBuilder.DropTable(
                name: "QUAN_LY_TON_KHO");

            migrationBuilder.DropTable(
                name: "SAN_PHAM_YEU_THICH");

            migrationBuilder.DropTable(
                name: "THANH_TOAN");

            migrationBuilder.DropTable(
                name: "GIO_HANG");

            migrationBuilder.DropTable(
                name: "KHUYEN_MAI");

            migrationBuilder.DropTable(
                name: "XUAT_KHO");

            migrationBuilder.DropTable(
                name: "NHAP_KHO");

            migrationBuilder.DropTable(
                name: "SAN_PHAM");

            migrationBuilder.DropTable(
                name: "DON_HANG");

            migrationBuilder.DropTable(
                name: "TAI_kHOAN");

            migrationBuilder.DropTable(
                name: "HANG_SAN_XUAT");

            migrationBuilder.DropTable(
                name: "PHUONG_THUC_THANH_TOAN");

            migrationBuilder.DropTable(
                name: "PHUONG_THUC_GD");

            migrationBuilder.DropTable(
                name: "DANH_MUC");
        }
    }
}
