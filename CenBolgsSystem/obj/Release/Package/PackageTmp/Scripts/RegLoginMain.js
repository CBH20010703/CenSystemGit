
function StratAjax(url) {
	layui.use('form', function () {
		var form = layui.form;
		form.on('submit(demo1)', function (data) {
			$.ajax({
				type: "post",
				url: url,
				data: data.field,
				success: function (res) {
					if (res.code == 0) {
						layer.msg(res.msg, { icon: 1 })
						return
					}
					layer.msg(res.msg, { icon: 2 })
                }
            })
			return false;
		});

	});
}