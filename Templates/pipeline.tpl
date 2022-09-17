### Gitlab-CI Pipeline 通知
> 项目：<font color="comment">[{{model.project.name}}]({{model.project.web_url}})</font>
> 状态：<font color="comment">{{model.object_attributes.detailed_status}}</font>
> 耗时：<font color="comment">{{model.object_attributes.duration}}</font>
> 分支：<font color="comment">{{model.object_attributes.ref}}</font>
> 提交：<font color="comment">[{{model.commit.title}}]({{model.commit.url}})</font>
> 提交人：<font color="comment">[{{model.user.name}}](email:{{model.user.email}})</font>

### Builds
{{ for build in model.builds }}
> 任务：<font color="comment">{{build.name}}</font>
> 状态：<font color="comment">{{build.status}}</font>
{{ end }}