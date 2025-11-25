const params = new URLSearchParams(window.location.search);

const maSV = params.get("maSV");
console.log(maSV)

let sinhViens = [];
const apiUrl = "http://localhost:5117/";
const table = document.querySelector("#tableDS tbody");
if (sinhViens == null || sinhViens.length === 0) {
  const tr = document.createElement("tr");
  tr.innerHTML = '<td colspan="9" class="text-center">No Record Found.</td>';
  table.appendChild(tr);
}


const loadTable = (list) => {
  table.innerHTML = "";
  if (list !== null && list.length > 0) {

    list.map((element, index) => {
      const tr = document.createElement("tr");
      tr.innerHTML = `<td class="text-center">${index + 1}</td>
                      <td>${element.maSV}</td>
                      <td>${element.hoTen}</td>
                      <td>${element.ngaySinh}</td>
                       <td>${element.gioiTinh}</td>
                      <td>${element.lop}</td>
                      <td>${element.email}</td>
                      <td class="text-center"><a class="text-success" onclick="onEdit(${element.maSV})">Edit</a></td>
                     <td class="text-center"><a class="text-danger" onclick="onDelete(${element.maSV})">Delete</a></td>`
      table.appendChild(tr);
    });

  }
}

const onLoadData = async () => {

  const onFetchData = await fetch(`${apiUrl}api/SinhVien/GetAll`);
  if (!onFetchData.ok) {
    alert(`API Lỗi: ${resp.status}`);
    return;
  }

  resp = await onFetchData.json();
  sinhViens = resp.data;
  loadTable(sinhViens);
}


const onEdit = (id) => {
  console.log(id);
  //const { maSV, ...updateObj } = obj;
}

const onDelete = async (maSV) => {
  if (maSV) {
    let status = confirm(`bạn có muốn xóa masv: ${maSV}`);
    if (status) {
      const onFetchDelete = await fetch(`${apiUrl}api/SinhVien/Delete/${maSV}`, { method: "DELETE" });
      if (!onFetchDelete.ok) {
        alert(`API Lỗi: ${resp.status}`);
        return;
      }
      sinhViens = sinhViens.filter(x => x.maSV !== maSV);
      loadTable(sinhViens);
    }
  }
}



// let dsSinhVienDangKy = [];
// if (dsSinhVienDangKy.length == 0)
//   $("#tableDS tbody").append('<tr><td colspan="7" class="text-center">Chưa Có Sinh Viên</td></tr>')

// const onDangKy = (event) => {
//   // # => id , . => class , val() => value, html('') => innerHTML('')
//   event.preventDefault();
//   $("input[name='chkDSMH']:checked").map((ind, item) => {
//     return item.value
//   })
//   let svObj = {
//     mssv: $("#txtMSSV").val(),
//     hoVaTen: $("#txtHoTen").val(),
//     maLop: $("#optMaLop").val(),
//     heHoc: $("input[name='rdoHeHoc']:checked")[0]?.value,
//     dsmh: $("input[name='chkDSMH']:checked").map((ind, item) => { return item.value })?.get()?.join(", ")
//   };

//   if (svObj.dsmh === null || svObj.dsmh.length == 0) {
//     alert(`vui lòng chọn môn học`)
//     return;
//   }

//   let ind = dsSinhVienDangKy.findIndex(x => x.mssv === svObj.mssv);
//   if (ind >= 0) {
//     alert(`MSSV: ${ svObj.mssv }, đã tồn tại`)
//     return;
//   }

//   dsSinhVienDangKy.push(svObj)

//   if (dsSinhVienDangKy.length == 1)
//     $("#tableDS tbody").empty();

//   let tr = `< tr >
//         <td>${dsSinhVienDangKy.length}</td>
//         <td>${svObj.mssv}</td>
//         <td>${svObj.hoVaTen}</td>
//         <td>${svObj.maLop}</td>
//         <td>${svObj.heHoc}</td>
//         <td>${svObj.dsmh}</td>
//         <td class="text-center"><a class="text-danger" onclick="onDelete('${svObj.mssv}')">Xóa</a></td>
//     `

//   $("#tableDS tbody").append(tr)

// }

// const onDelete = (mssv) => {
//   let resp = confirm(`Bạn có muốn xóa mssv: ${mssv}`);
//   if (resp) {
//     console.log('ddd')
//     let ind = dsSinhVienDangKy.findIndex(x => x.mssv === mssv);
//     if (ind === -1) {
//       alert(`MSSV: ${mssv}, không tồn tại`)
//       return;
//     }
//     $("#tableDS tbody").empty()
//     let tr = "";
//     dsSinhVienDangKy.splice(ind, 1);
//     dsSinhVienDangKy.map((item, ind) => {
//       tr += `<tr>
//                 <td>${ind + 1}</td>
//                 <td>${item.mssv}</td>
//                 <td>${item.hoVaTen}</td>
//                 <td>${item.maLop}</td>
//                 <td>${item.heHoc}</td>
//                 <td>${item.dsmh}</td>
//                 <td class="text-center"><a class="text-danger" onclick="onDelete('${item.mssv}')">Xóa</a></td>
//              </tr>`
//     })
//     $("#tableDS tbody").append(tr)
//   }
//   else console.log('no')
// }



