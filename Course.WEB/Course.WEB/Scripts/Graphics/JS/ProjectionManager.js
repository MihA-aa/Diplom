function ProjectionManager(mediator, projectionPointsDrawer, stylesManager, projectionParams) {
    this.projections = [];
    this.graphics = [];
    this.testMode = true;
    this.tasks = [
        [
            {
                projection: new Projection('point', [new Point3D(90, 90, 90)]),
            },
            {
                projection: new Projection('point', [new Point3D(120, 120, 120)]),

            },
        ],
        [
            {
                projection: new Projection('point', [new Point3D(90, 90, 90)]),
                comparer:'x>y>z>'
            },
        ],
    ]
    ;

    this.bind = function (point, shape) {
        return this.projections.find(function (proj) {
            if(proj.shape != shape)
            {
                return false;
            }
            var point3D = proj.get3DPoint(point);
            var nearest = proj.getNearestPoint(point3D);
            if (nearest != null) {
                return true;
            }
        });
	};

    this.getTasks = function () {
		var matches = document.referrer.match("http:\/\/localhost:9847\/Graphic\\?taskId=([0-9]+)");
		var graphicTaskId = matches && matches[0]
		var projectionManager = this;
		if(graphicTaskId){
			return new Promise(function(resolve) {
				$.ajax({
					type: 'GET',
					url: '../../Graphic/Get?taskId=' + graphicTaskId,
					success: function(data){
						projectionManager.tasks = data.Projections.map(function (projection) {
							return mapProjection(projection);
						});
						resolve(projectionManager.getTaskText(projectionManager.tasks));
					}
				});
			});
		} else {
			return new Promise(function(resolve, reject) { reject});
		}
    };
    var shapeNames =
        {
            point: 'точку',
            line: 'отрезок',
            polygon: 'многоугольник',
            ellipse: 'эллипс',
        };
    var shapeNamesR =
        {
            point: 'точки',
            line: 'отрезка',
            polygon: 'многоугольника',
            ellipse: 'эллипса',
		};
    this.getTaskText = function (task) {
        var result = "";
        result += 'Построить ';

        task.forEach(function (subtask) {
                result += shapeNames[subtask.projection.shape] + ' ';
            var conditions = this.parseConditions(subtask.comparer);
            var conditionStrArr = [];
            if(conditions.x === '>')
            {
                conditionStrArr.push('правее')
            }
            if(conditions.x === '<')
            {
                conditionStrArr.push('левее')
            }
            if(conditions.y === '>')
            {
                conditionStrArr.push('ближе')
            }
            if(conditions.y === '<')
            {
                conditionStrArr.push('дальше')
            }
            if(conditions.z === '>')
            {
                conditionStrArr.push('выше')
            }
            if(conditions.z === '<')
            {
                conditionStrArr.push('ниже')
            }
            if(conditionStrArr.length > 0)
            {
                if(conditionStrArr.length == 1)
                {
                    result += conditionStrArr[0];
                }
                if(conditionStrArr.length == 2)
                {
                    result += conditionStrArr[0] + ' и ' + conditionStrArr[1];
                }
                if(conditionStrArr.length == 3)
                {
                    result += conditionStrArr[0] + ', ' +conditionStrArr[1] + ' и ' + conditionStrArr[2];
                }
                result +=  ' ' + shapeNamesR[subtask.projection.shape] + ' ';
            }
            result += 'с координатами ';
            subtask.projection.points3D.forEach(function (p, i, arr) {
                result += p.toString();
                if (i != arr.length - 1) {
                    result += ','
                }
                result += ' ';
            });
            result += ', ';
        }.bind(this));
        return result;
    };
    this.parseConditions = function(conditions)
    {
        if(!conditions) return {
            x:'=',
            y:'=',
            z:'=',
        };
        var res=
            {
                x:conditions[1],
                y:conditions[3],
                z:conditions[5],
            };
        return res;
    };
    this.getShapeText = function (task) {
        var result = "";
        if (task.shape == 'polygon') {
            if (task.points3D.length == 1) {
                result += 'точка ';
            }
            if (task.points3D.length == 2) {
                result += 'отрезок ';
            }
            if (task.points3D.length == 3) {
                result += 'многоугольник ';
            }
            task.points3D.forEach(function (p, i, arr) {
                result += p.toString();
                if (i != arr.length - 1) {
                    result += ','
                }
                result += ' ';
            });
        }
        return result;
    };

    this.validateTask = function (taskIndex) {
        var results = [];
        var resolvedProjections = [];
        var unresolvedProjections = this.projections.slice(0);
        var unresolvedTasks = this.tasks.filter(function (taskproj) {
            var match = unresolvedProjections.find(function (p) {
                return p.getMatches(taskproj) == taskproj.projection.points3D.length * 3 && p.shape === taskproj.projection.shape && p.points3D.length == taskproj.projection.points3D.length * 3;
            });
            if (match) {
                unresolvedProjections.splice(unresolvedProjections.indexOf(match), 1);
                results.push(match.validateTask(taskproj));
                resolvedProjections.push(match);
                return false;
            }
            return true;
        }.bind(this));

        unresolvedTasks = unresolvedTasks.filter(function (taskproj) {
            var match = unresolvedProjections.filter(function (p) {
                return p.shape === taskproj.projection.shape;
            }).sort(function (a, b) {
                return b.getMatches(taskproj) - a.getMatches(taskproj);
            })[0];
            if (match && match.getMatches(taskproj) != 0) {
                unresolvedProjections.splice(unresolvedProjections.indexOf(match), 1);
                results.push(match.validateTask(taskproj));
                return false;
            }
            return true;
        });
        unresolvedTasks.forEach(function (t) {
            var points = t.projection.points3D.map(function (p) {
                var res =
                    {
                        point: p,
                        xy: false,
                        xz: false,
                        yx: false
                    };
                return res;
            });
            results.push({
                shape: t.projection.shape,
                points: points,
            });
        }.bind(this));

        mediator.publish("projectionsChanged");
        return results;
    };
    this.deleteSelectedProjections = function () {
        project.selectedItems.forEach(function (selectedItem) {
            var index = this.projections.indexOf(selectedItem.data.projection);
            if (index >= 0) {
                this.projections[index].isDeleted = true;
                this.projections.splice(index, 1);
            }
        }.bind(this));
        mediator.publish("projectionsChanged");
    };
    this.removeInvalidProjections = function () {
        this.projections = this.projections.filter(
            function (p1) {

            }
        )
    }
}

function mapProjection(projection) {
	projection.shape = mapShape(projection.Shape);
	var point = projection.Points[0]
	return {
		projection: new Projection(projection.shape, [new Point3D(point.X, point.Y, point.Z)]),
	}
}

function mapShape(shape) {
	switch (shape) {
		case 0: return "point";
		case 1: return "line";
		case 2: return "polygon";
		case 3: return "ellipse";
}}
