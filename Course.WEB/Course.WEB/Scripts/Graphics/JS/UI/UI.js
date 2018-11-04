$(document).ready(function () {
    var constColor = {
        'black': '#000000',
        'white': '#ffffff',
        'red': '#FF0000',
        'default': '#777777',
        'primary': '#337ab7',
        'success': '#5cb85c',
        'info': '#5bc0de',
        'warning': '#f0ad4e',
        'danger': '#d9534f'
    };
	const isInFrame = self !== top;
	if(isInFrame){
		$("#projectionsSettings").hide();
		$("#setTaskProjection").show();
	}
	else{
		timer.addEventListener('secondsUpdated', function (e) {
			$('#pageTimer').html(timer.getTimeValues().toString());
		});
		
	}
    $('.draggable').draggable({containment: "parent"});

    $('.taskResultsPanel').hide();

    $('.toolboxMain li').click(function () {
        var tool = $(this).attr('data-tool');
        toolbox[tool].activate();
    });
    $('.toolbox3D li').click(function () {
        var tool = $(this).attr('data-tool');
        toolbox3D[tool].activate();
    });

    mediator.subscribe("toolActivated", function (tool) {
        $('[data-tool].active').removeClass('active');
        $('.toolboxMain [data-tool=' + tool + ']').addClass('active');
    });

    mediator.subscribe("tool3DActivated", function (tool) {
        $('[data-tool].active').removeClass('active');
        $('.toolbox3D [data-tool=' + tool + ']').addClass('active');
    });

    $(".colorpicker-component[data-setting]").colorpicker({
        colorSelectors: constColor,
    }).on('hidePicker', function () {
        settingsManager.setValue($(this).attr('data-setting'), $(this).colorpicker('getValue'));
    });
    $(".colorpicker-component[data-setting]").each(function (i, item) {
        var val = settingsManager.getValue($(item).attr('data-setting'));
        $(item).colorpicker('setValue', val);
    });

    $('.navbar-btn').click(function () {
        var navId = $(this).attr('id');
        var tab = $("[data-nav-id=" + navId + "]");
        if (tab.is(":visible")) {
            tab.hide("fast");
        }
        else {
            $("[data-nav-id]:visible").hide();
            tab.show();
        }
    });


	projectionManager.getTasks()
		.then((task) => {
				$('.taskText').append(task);
		})
		.catch(() => {});

    // $('.tasksList li').click(function () {
    //     $('.tasksList li').removeClass('active');
    //     $(this).addClass('active');
    //     $('.taskText').text($(this).text());
    //     projectionManager.testMode = true;
    // });
    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
    $('.taskValidate').click(function () {
        $('.taskResultsPanel').show();
        // var index = parseInt($('.tasksList li.active').first().val());
        var res = projectionManager.validateTask();
        var items = $('.taskErrors>li:not(:first-child)').each(function () {
            $(this).remove();
        });
        var list = $('.taskErrors');
        res.forEach(function (result) {
            var projValid = true;
            var pointsHtml = result.points.map(function (p) {
                if (p.xy && p.xz && p.yz) {
                    return '<li class="taskTrue">' + p.point.toString() + '<span class="glyphicon glyphicon-ok"></span></li>';
                }
                else if (!p.xy && !p.xz && !p.yz) {
                    projValid = false;
                    return '<li class="taskFalse">' + p.point.toString() + '<span class="glyphicon glyphicon-remove"></span></li>';
                }
                else {
                    projValid = false;
                    var xyHtml = p.xy ? '<span class="taskTrue">XY<span class="glyphicon glyphicon-ok"></span></span>' : '<span class="taskFalse">XY<span class="glyphicon glyphicon-remove"></span></span>';
                    var xzHtml = p.xz ? '<span class="taskTrue">XZ<span class="glyphicon glyphicon-ok"></span></span>' : '<span class="taskFalse">XZ<span class="glyphicon glyphicon-remove"></span></span>';
                    var yzHtml = p.yz ? '<span class="taskTrue">YZ<span class="glyphicon glyphicon-ok"></span></span>' : '<span class="taskFalse">YZ<span class="glyphicon glyphicon-remove"></span></span>';
                    return '<li class="taskFalse">' + p.point.toString() + ' ' + xyHtml + xzHtml + yzHtml + '</li>';
                }
            }).join('');
            var liClass = result.points
            list.append('<li class="'+(projValid?'taskTrue':'taskFalse')+'">' + capitalizeFirstLetter(result.shape) + '<ul>'+pointsHtml+'</ul></li>');
        });
    });

    $('#setTaskProjection').click(function () {
		const shape = 'point';
		const isValid = projectionManager.isValidProjection(shape);
		if(!isValid){
			$("#isValidMessage").show();
			return;
		}
		var values = projectionManager.getProjectionValues(shape);
		$('#outputIframeData', window.parent.document).text(JSON.stringify(values));
		$(".ui-dialog-titlebar-close", window.parent.document).first().click();
    });

    $('.taskCancel').click(function () {
		history.back();
    });

    $('.taskFinish').click(function () {
		var graphicTaskId = document.referrer.match("http:\/\/localhost:9847\/Graphic\\?taskId=([0-9]+)")[1];
		var isSolved = projectionManager.validateTask().every(projection => projection.isValid);
		var seconds = timer.getTimeValues().seconds;
		$.ajax({
			type: 'GET',
			url: '../../Task/SolveGraphic?taskId=' + graphicTaskId + '&isSolved=' + isSolved + '&secondsToSolve=' + seconds,
		  });
    });


    $("input[type=number][data-setting]").change(function () {
        settingsManager.setValue($(this).attr("data-setting"), parseInt($(this).val()));
    });
    $("input[type=number][data-setting]").each(function () {
        var val = settingsManager.getValue($(this).attr("data-setting"));
        $(this).val(val);
    });


    $("input[type=checkbox][data-setting]").click(function () {
        if (!$(this).is(':checked')) {
            settingsManager.setValue($(this).attr("data-setting"), false);
        }
        else {
            settingsManager.setValue($(this).attr("data-setting"), true);
        }
    });
    $("input[type=checkbox][data-setting]").each(function () {
        var val = settingsManager.getValue($(this).attr("data-setting"));
        $(this).prop('checked', val);
    });

    $("input[data-slider-min]").bootstrapSlider();
    $("input[data-slider-min]").on("slide", function (slideEvt) {
        settingsManager.setValue($(this).attr('data-setting'), slideEvt.value);
        $(this).siblings(".valueLabel").text(slideEvt.value);
    });
    $("input[data-slider-min]").each(function (i, item) {
        var val = settingsManager.getValue($(item).attr('data-setting'));
        $(item).bootstrapSlider('setValue', val);
        $(item).siblings(".valueLabel").text(val);
    });

    $("select[data-setting]").change(function () {
        var value = $(this).val();
        settingsManager.setValue($(this).attr('data-setting'), value);
    });
    $("select[data-setting]").each(function () {
        var val = settingsManager.getValue($(this).attr("data-setting"));
        $(this).val(val);
    });
    $("[data-for-tool]").each(function (i, item) {
        $(item).hide();
        mediator.subscribe('toolActivated',function (tool) {
            var a =$(item).attr('data-for-tool');
           if(tool == $(item).attr('data-for-tool'))
           {
               $(item).show();
           }
           else
           {
               $(item).hide();
           }
        });
    });
    $('#controlLayersPanel').click(function () {
        $('#layersPanel').toggle();
    });
    $('#controlElementPanel').click(function () {
        $('#leftmenu').toggle();
    });

});
