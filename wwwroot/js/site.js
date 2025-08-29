$(function(){
  // Books
  $("#btnAdd").on("click", function(){
    $("#bookModal").modal("show");
    $("#bookModalBody").load("/Books/Create");
  });
  $(document).on("click", ".btn-edit", function(){
    const id = $(this).data("id");
    $("#bookModal").modal("show");
    $("#bookModalBody").load("/Books/Edit/" + id);
  });
  $(document).on("click", ".btn-delete", function(){
    if(!confirm("Delete this book?")) return;
    const id = $(this).data("id");
    $.post("/Books/Delete/" + id)
      .done(()=>location.reload())
      .fail((xhr)=>alert(xhr.responseJSON?.message ?? "Error"));
  });

  // Authors
  $("#btnAddAuthor").on("click", function(){
    $("#authorModal").modal("show");
    $("#authorModalBody").load("/Authors/Create");
  });
  $(document).on("click", ".btn-edit-author", function(){
    const id = $(this).data("id");
    $("#authorModal").modal("show");
    $("#authorModalBody").load("/Authors/Edit/" + id);
  });
  $(document).on("click", ".btn-del-author", function(){
    if(!confirm("Delete this author?")) return;
    const id = $(this).data("id");
    $.post("/Authors/Delete/" + id).done(()=>location.reload()).fail(()=>alert("Error"));
  });

  // Genres
  $("#btnAddGenre").on("click", function(){
    $("#genreModal").modal("show");
    $("#genreModalBody").load("/Genres/Create");
  });
  $(document).on("click", ".btn-edit-genre", function(){
    const id = $(this).data("id");
    $("#genreModal").modal("show");
    $("#genreModalBody").load("/Genres/Edit/" + id);
  });
  $(document).on("click", ".btn-del-genre", function(){
    if(!confirm("Delete this genre?")) return;
    const id = $(this).data("id");
    $.post("/Genres/Delete/" + id).done(()=>location.reload()).fail(()=>alert("Error"));
  });
});
