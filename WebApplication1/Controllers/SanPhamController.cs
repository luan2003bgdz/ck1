using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SanPhamController : ApiController
    {
        CSDLTestEntities db = new CSDLTestEntities();
        [HttpGet]
        public List<SANPHAM> getAll()
        {
            return db.SANPHAMs.ToList();
        }
        [HttpGet]
        public List<SANPHAM> findByMaDM(int madm)
        {
            return db.SANPHAMs.Where(x => x.MaDM == madm).ToList();
        }
        [HttpGet]
        public SANPHAM getSANPHAM(int masp)
        {
            return db.SANPHAMs.FirstOrDefault(x => x.MaSP == masp);
        }
        [HttpPost]
        public bool themSanPham(int masp, string tensp, int gia, int madm)
        {
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                SANPHAM sp1 = new SANPHAM();
                sp1.MaSP = masp;
                sp1.TenSP = tensp;
                sp1.Gia = gia;
                sp1.MaDM = madm;
                db.SANPHAMs.Add(sp1);
                db.SaveChanges();
                return true;

            }
            return false;
        }
        [HttpPut]
        public bool suaSanPham(int masp, string tensp, int gia, int madm)
        {
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(x => x.MaSP == masp);
            if (sp != null)
            {
                sp.TenSP = tensp;
                sp.Gia = gia;
                sp.MaDM = madm;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpDelete]
        public bool xoaSanPham(int masp)
        {
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(x => x.MaSP == masp);
            if (sp != null)
            {
                db.SANPHAMs.Remove(sp);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
