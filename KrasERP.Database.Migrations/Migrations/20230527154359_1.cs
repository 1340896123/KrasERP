using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KrasERP.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kras");

            migrationBuilder.CreateTable(
                name: "form",
                schema: "kras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "itemtype",
                schema: "kras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    label = table.Column<string>(type: "text", nullable: true),
                    labelplural = table.Column<string>(type: "text", nullable: true),
                    system = table.Column<bool>(type: "boolean", nullable: true),
                    iconname = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true),
                    recordscreenidfield = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itemtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "property",
                schema: "kras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    label = table.Column<string>(type: "text", nullable: true),
                    placeholdertext = table.Column<string>(type: "text", nullable: true),
                    gridwidth = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    helptext = table.Column<string>(type: "text", nullable: true),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    unique = table.Column<bool>(type: "boolean", nullable: false),
                    index = table.Column<bool>(type: "boolean", nullable: false),
                    searchable = table.Column<bool>(type: "boolean", nullable: false),
                    auditable = table.Column<bool>(type: "boolean", nullable: false),
                    system = table.Column<bool>(type: "boolean", nullable: false),
                    enablesecurity = table.Column<bool>(type: "boolean", nullable: false),
                    sourceid = table.Column<Guid>(type: "uuid", nullable: false),
                    related = table.Column<Guid>(type: "uuid", nullable: false),
                    regex = table.Column<string>(type: "text", nullable: true),
                    propertydatatype = table.Column<int>(type: "integer", nullable: false),
                    defaultvalue = table.Column<string>(type: "text", nullable: true),
                    length = table.Column<int>(type: "integer", nullable: false),
                    format = table.Column<string>(type: "text", nullable: true),
                    usecurrenttimeasdefaultvalue = table.Column<bool>(type: "boolean", nullable: true),
                    canmulteselect = table.Column<bool>(type: "boolean", nullable: false),
                    datasource = table.Column<Guid>(type: "uuid", nullable: false),
                    precision = table.Column<int>(type: "integer", nullable: false),
                    scale = table.Column<int>(type: "integer", nullable: false),
                    foreignproperty = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_property_itemtype_sourceid",
                        column: x => x.sourceid,
                        principalSchema: "kras",
                        principalTable: "itemtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "view",
                schema: "kras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    sortorder = table.Column<int>(type: "integer", nullable: false),
                    form_classification = table.Column<string>(type: "text", nullable: true),
                    sourceid = table.Column<Guid>(type: "uuid", nullable: false),
                    relatedid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_view", x => x.id);
                    table.ForeignKey(
                        name: "fk_view_form_relatedid",
                        column: x => x.relatedid,
                        principalSchema: "kras",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_view_itemtype_sourceid",
                        column: x => x.sourceid,
                        principalSchema: "kras",
                        principalTable: "itemtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "field",
                schema: "kras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    label = table.Column<string>(type: "text", nullable: true),
                    css = table.Column<string>(type: "text", nullable: true),
                    fieldtype = table.Column<int>(type: "integer", nullable: false),
                    propertyinfoid = table.Column<Guid>(type: "uuid", nullable: false),
                    sourceid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_field", x => x.id);
                    table.ForeignKey(
                        name: "fk_field_form_sourceid",
                        column: x => x.sourceid,
                        principalSchema: "kras",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_field_property_propertyinfoid",
                        column: x => x.propertyinfoid,
                        principalSchema: "kras",
                        principalTable: "property",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "kras",
                table: "form",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("693527c1-3f8b-cb70-73b6-8289642cc4ea"), "", "ItemType" });

            migrationBuilder.InsertData(
                schema: "kras",
                table: "itemtype",
                columns: new[] { "id", "color", "iconname", "label", "labelplural", "name", "recordscreenidfield", "system" },
                values: new object[] { new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), "", "", "对象类", "对象类", "ItemType", null, true });

            migrationBuilder.InsertData(
                schema: "kras",
                table: "property",
                columns: new[] { "id", "auditable", "canmulteselect", "datasource", "defaultvalue", "description", "enablesecurity", "foreignproperty", "format", "gridwidth", "helptext", "index", "label", "length", "name", "placeholdertext", "precision", "propertydatatype", "regex", "related", "required", "scale", "searchable", "sourceid", "system", "unique", "usecurrenttimeasdefaultvalue" },
                values: new object[,]
                {
                    { new Guid("47aa96b0-b463-44a9-96f6-073a2f0afe01"), false, false, new Guid("00000000-0000-0000-0000-000000000000"), null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, null, false, "ID", 0, "id", null, 0, 13, null, new Guid("00000000-0000-0000-0000-000000000000"), true, 0, true, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), true, true, null },
                    { new Guid("71deb1ee-0eed-a0d5-d512-4e248e9a2e44"), false, false, new Guid("00000000-0000-0000-0000-000000000000"), null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, null, false, "复数标签", 0, "labelplural", null, 0, 2, null, new Guid("00000000-0000-0000-0000-000000000000"), true, 0, true, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), true, true, null },
                    { new Guid("71deb1ee-0eed-a0d5-d512-4e248e9b2e44"), false, false, new Guid("00000000-0000-0000-0000-000000000000"), null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, null, false, "KeyedName", 0, "keyedname", null, 0, 2, null, new Guid("00000000-0000-0000-0000-000000000000"), true, 0, true, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), true, true, null },
                    { new Guid("71deb1ee-0eed-a0d5-d512-4e249e9a2e44"), false, false, new Guid("00000000-0000-0000-0000-000000000000"), null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, null, false, "单数标签", 0, "label", null, 0, 2, null, new Guid("00000000-0000-0000-0000-000000000000"), true, 0, true, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), true, true, null },
                    { new Guid("d47693ab-dacc-1b74-35ee-1cb70750e827"), false, false, new Guid("00000000-0000-0000-0000-000000000000"), null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, null, false, "名称", 0, "name", null, 0, 2, null, new Guid("00000000-0000-0000-0000-000000000000"), true, 0, true, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), true, true, null }
                });

            migrationBuilder.InsertData(
                schema: "kras",
                table: "view",
                columns: new[] { "id", "name", "relatedid", "sortorder", "sourceid", "form_classification" },
                values: new object[] { new Guid("7f7d7ab8-f1db-f8d7-3fe6-e05f946fd904"), null, new Guid("693527c1-3f8b-cb70-73b6-8289642cc4ea"), 0, new Guid("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91"), null });

            migrationBuilder.InsertData(
                schema: "kras",
                table: "field",
                columns: new[] { "id", "css", "fieldtype", "label", "name", "propertyinfoid", "sourceid" },
                values: new object[,]
                {
                    { new Guid("421ba6c3-1798-cdac-15bf-2476fba899a1"), null, 0, "复数标签", "labelplural", new Guid("71deb1ee-0eed-a0d5-d512-4e248e9a2e44"), new Guid("693527c1-3f8b-cb70-73b6-8289642cc4ea") },
                    { new Guid("dec6908c-bd9b-b1e7-40bb-deb6655b70c5"), null, 0, "名称", "name", new Guid("d47693ab-dacc-1b74-35ee-1cb70750e827"), new Guid("693527c1-3f8b-cb70-73b6-8289642cc4ea") },
                    { new Guid("fd3b2635-4abf-0040-fcac-c07761c61e01"), null, 0, "单数标签", "label", new Guid("71deb1ee-0eed-a0d5-d512-4e249e9a2e44"), new Guid("693527c1-3f8b-cb70-73b6-8289642cc4ea") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_field_propertyinfoid",
                schema: "kras",
                table: "field",
                column: "propertyinfoid");

            migrationBuilder.CreateIndex(
                name: "IX_field_sourceid",
                schema: "kras",
                table: "field",
                column: "sourceid");

            migrationBuilder.CreateIndex(
                name: "IX_property_sourceid",
                schema: "kras",
                table: "property",
                column: "sourceid");

            migrationBuilder.CreateIndex(
                name: "IX_view_relatedid",
                schema: "kras",
                table: "view",
                column: "relatedid");

            migrationBuilder.CreateIndex(
                name: "IX_view_sourceid",
                schema: "kras",
                table: "view",
                column: "sourceid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "field",
                schema: "kras");

            migrationBuilder.DropTable(
                name: "view",
                schema: "kras");

            migrationBuilder.DropTable(
                name: "property",
                schema: "kras");

            migrationBuilder.DropTable(
                name: "form",
                schema: "kras");

            migrationBuilder.DropTable(
                name: "itemtype",
                schema: "kras");
        }
    }
}
