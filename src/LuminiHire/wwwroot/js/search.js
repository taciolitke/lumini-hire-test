

$("#search-btn").on("click", search);
$(".page-link").on("click", paginationClick);

const pageSize = 10;
let currentPage = 0;
const firstPage = 0;
const lastPage = 99999;

function search() {
    searchWithPagination(firstPage);
}

function paginationClick(){
    const page = parseInt($(this).html());

    let pagination = 0;

    if(this.id === 'item-previous') {
        pagination = currentPage > firstPage? currentPage - 1 : firstPage;
        verifyPreviousActive();
    }
    else if(this.id === 'item-next') {
        pagination = currentPage < lastPage? currentPage + 1 : lastPage;
        verifyNextActive();
    }
    else {
        pagination = page;
        $(".page-item").removeClass("active");
        $(".page-"+this.id).addClass("active");
    }
    currentPage = pagination;
    searchWithPagination(currentPage);
}

function verifyPreviousActive() {
    if($(".page-item-second").hasClass("active")) {
        $(".page-item").removeClass("active");
        $(".page-item-first").addClass("active")
    }
    else if($(".page-item-third").hasClass("active")){
        $(".page-item").removeClass("active");
        $(".page-item-second").addClass("active")
    }
}

function verifyNextActive() {
    if($(".page-item-first").hasClass("active")) {
        $(".page-item").removeClass("active");
        $(".page-item-second").addClass("active")
    }
    else if($(".page-item-second").hasClass("active")){
        $(".page-item").removeClass("active");
        $(".page-item-third").addClass("active")
    }
}

function searchWithPagination(page){

    $("#search-btn").addClass("d-none");
    $("#load-search-btn").removeClass("d-none");

    const idFilter = $("#id-filter").val();
    const query = $("#query-filter").val();

    var request = new XMLHttpRequest();

    request.open('GET', `/api/search?${idFilter && 'unitId='+idFilter}&query=${query}&skip=${pageSize * page}&take=${pageSize}`, true);

    request.onload = function () {
        console.log(this.response);
        var data = this.response ? JSON.parse(this.response) : new Array();
        getWithSuccess(data);
    }
    request.send();
}

function getWithSuccess(data) {


    $("#search-btn").removeClass("d-none");
    $("#load-search-btn").addClass("d-none");

    const recordsBodyId = "#search-result";

    $(recordsBodyId).html("");

    $(data).each(function (index) {
        $(recordsBodyId).append("<tr><td>" + this.unitId + "</td><td>" + this.city + "</td><td>" + this.instituition + "</td><td>" + this.zip + "</td></tr>");
    });

    if(data.length == 0){
        $(recordsBodyId).append("<tr><td colspan='4' class='text-center'>Nenhum registro encontrado</td></tr>");
    }

}
