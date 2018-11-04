$(document).ready(function () {
	$("#dialog").dialog({
		autoOpen: false, modal: true, height: "800px", width: "1500px", minHeight: "800px;",
		minWidth: "1500px;", title: "Выберите проекцию", resizable: false,
		open: function(ev, ui){
			$('#GraphIframe').attr('src','http://localhost:9847/Scripts/Graphics/index.html');
		 },
		 close: function(ev, ui){
			const outputData = $('#outputIframeData').text();
			if(outputData){
				parseOutputData(outputData);
				$('#outputIframeData').text('')
			}
		 }
	});
});

var graphDialogInitiatorId

$('.isGraphic').change(function () {
    if ($(this).is(':checked')) {
        $(".graphic-form").show("slow");
    } else {
        $(".graphic-form").hide("slow");
    }
});

$("#add-projection").click(function () {
	const projectionIndex = $(".projection").length;
	$(getNewProjection(projectionIndex, 0)).appendTo("#projections").hide().show('slow');
});

$("#delete-projection").click(function () {
	$('#projections .projection').last().hide('slow', function(){ $(this).remove(); });
});

function onChangeProjectionType(select)
{
	var elem = $(select);
	const projectionId = elem.parent().attr("index")
	elem.next('.points').remove();
	elem.after(getPoints(projectionId, elem.val()));
}

function getNewProjection(projectionId, shape) {
	return `
		<div class="projection" index="${projectionId}">
			<button type="button" class="btn btn-default btn-mine" onClick="openGraphDialog(this)" style="padding: 0px 7px; position: absolute; right: 10px">
				<span class="glyphicon glyphicon-picture" aria-hidden="true" style="font-size: 20px;">
				</span>
			</button>
			<select name="GraphicTask.Projections[${projectionId}].Shape" class="form-control projection-type" onChange="onChangeProjectionType(this)">
				<option selected value="0">Точка</option>
				<option value="1">Линия</option>
				<option value="2" disabled>Многоугольник</option>
				<option value="3" disabled>Эллипс</option>
			</select>
			${getPoints(projectionId, shape)}
		</div>`;
}

function openGraphDialog(button){
	var but = $(button);
	graphDialogInitiatorId = but.parent().attr("index")
	$("#dialog").dialog('open');
}

function parseOutputData(data){
	var parsedData = JSON.parse(data);
	setPointValues(parsedData, graphDialogInitiatorId);
}

function setPointValues(point, index){
	const projection = $(`.projection[index=${index}]`);
	projection.find("#X").val(point.x);
	projection.find("#Y").val(point.y);
	projection.find("#Z").val(point.z);
}

function getPoints(projectionId, shape) {
	var result;
	switch (shape.toString()) {
		case "0": result = getNewPoint(projectionId, 0); break;
		case "1": result = `${getNewPoint(projectionId, 0, 1)}${getNewPoint(projectionId, 1, 2)}`; break;
		default: alert('sorry');
	  };
	  return `<div class="points">${result}</div>`
}

function getNewPoint(projectionId, pointId, order="") {
	return `
		<div class="point">
			<div class="form-group">
				<label for="X">X${order}:</label>
				<input class="form-control text-box single-line ordinate" data-val="true" data-val-length-max="3" data-val-length-min="1"
					data-val-required="X не может быть пустым" id="X" name="GraphicTask.Projections[${projectionId}].Points[${pointId}].X" type="text" value="">
				<span class="field-validation-valid" data-valmsg-for="X" data-valmsg-replace="true"></span>
			</div>
			<div class="form-group">
				<label for="Y">Y${order}:</label>
				<input class="form-control text-box single-line ordinate" data-val="true" data-val-length-max="3" data-val-length-min="1"
						data-val-required="Y не может быть пустым" id="Y" name="GraphicTask.Projections[${projectionId}].Points[${pointId}].Y" type="text" value="">
				<span class="field-validation-valid" data-valmsg-for="Y" data-valmsg-replace="true"></span>
			</div>
			<div class="form-group">
				<label for="Z">Z${order}:</label>
				<input class="form-control text-box single-line ordinate" data-val="true" data-val-length-max="3" data-val-length-min="1"
						data-val-required="Z не может быть пустым" id="Z" name="GraphicTask.Projections[${projectionId}].Points[${pointId}].Z" type="text" value="">
				<span class="field-validation-valid" data-valmsg-for="Z" data-valmsg-replace="true"></span>
			</div>
		</div>`;
}