/*********************************************************************** *
本文件由T4模板生成，请将本文件复制到前端YixiaoAdmin/views文件夹中使用 *
文件名：UserManagement.vue * 生成时间：12/23/2020 08:59:50
************************************************************************/
<template>
	<div class="container">
		<el-col :span="24" class="toolbar">
			<el-input
				style="width: 200px"
				placeholder="搜索名称"
				v-model="queryStr"
			></el-input
			>&nbsp;&nbsp;
			<el-button @click="query()">查询</el-button>
			<el-button type="danger" @click="refreshTable()"
				>刷新列表</el-button
			>
			<el-button type="primary" @click="changeDialogFormVisible()"
				>添加</el-button
			>
			<!-- <el-button type="danger" @click="createClassClick()">上传课件</el-button> -->
		</el-col>
		<el-table
			:data="tableData"
			style="width: 100%; min-height: 70vh"
			@sort-change="sortChange"
			:default-sort="{ prop: 'CreateTime', order: 'descending' }"
		>
			<el-table-column type="selection" width="55"></el-table-column>
			<el-table-column
				:show-overflow-tooltip="true"
				prop="Id"
				label="Id"
				width="220"
			></el-table-column>

			<el-table-column
				:show-overflow-tooltip="true"
				prop="UserName"
				label="用户名"
				width="220"
				sortable="custom"
			></el-table-column>
			<el-table-column
				:show-overflow-tooltip="true"
				prop="Password"
				label="密码"
				width="220"
				sortable="custom"
			></el-table-column>
			<el-table-column
				:show-overflow-tooltip="true"
				prop="Role.Name"
				label="用户类型"
				width="220"
			></el-table-column>
			<el-table-column
				:show-overflow-tooltip="true"
				prop="CreateTime"
				label="创建时间"
				width="220"
				sortable="custom"
			></el-table-column>
			<el-table-column fixed="right" label="操作" width="100">
				<template slot-scope="scope">
					<el-button
						@click="handleEdit(scope.row)"
						type="text"
						size="small"
						>编辑</el-button
					>
					<el-button
						@click="handleDelete(scope.row)"
						type="text"
						size="small"
						:disabled="deleteButtonDisabled"
						>删除</el-button
					>
				</template>
			</el-table-column>
		</el-table>
		<div class="block">
			<el-pagination
				@size-change="handleSizeChange"
				@current-change="handleCurrentChange"
				:current-page="currentPage"
				:page-sizes="pageSizes"
				:page-size="pageSize"
				layout="total, sizes, prev, pager, next, jumper"
				:total="totalNumber"
			></el-pagination>
		</div>
		<el-dialog title="添加" :visible.sync="addDialogFormVisible">
			<el-form :model="addForm">
				<el-form-item label="用户名" :label-width="formLabelWidth">
					<el-input
						v-model="addForm.UserName"
						autocomplete="off"
						placeholder="请输入用户名"
					></el-input>
				</el-form-item>
				<el-form-item label="密码" :label-width="formLabelWidth">
					<el-input
						v-model="addForm.Password"
						autocomplete="off"
						placeholder="请输入密码"
					></el-input>
				</el-form-item>
				<el-form-item label="用户类型" :label-width="formLabelWidth">
					<el-select
						v-model="addForm.RoleId"
						placeholder="请选择角色"
					>
						<el-option
							v-for="(item, i) in RoleList"
							:label="item.Name"
							:value="item.Id"
							:key="i"
						></el-option>
					</el-select>
				</el-form-item>
			</el-form>
			<div slot="footer" class="dialog-footer">
				<el-button @click="addDialogFormVisible = false"
					>取 消</el-button
				>
				<el-button
					type="primary"
					@click="AddConfirm()"
					:disabled="addButtonDisabled"
					>确 定</el-button
				>
			</div>
		</el-dialog>

		<el-dialog title="编辑" :visible.sync="editDialogFormVisible">
			<el-form :model="editForm">
				<el-form-item label="用户名" :label-width="formLabelWidth">
					<el-input
						v-model="editForm.UserName"
						autocomplete="off"
						placeholder="请输入用户名"
					></el-input>
				</el-form-item>
				<el-form-item label="密码" :label-width="formLabelWidth">
					<el-input
						v-model="editForm.Password"
						autocomplete="off"
						placeholder="请输入密码"
					></el-input>
				</el-form-item>
				<el-form-item label="用户类型" :label-width="formLabelWidth">
					<el-select
						v-model="editForm.RoleId"
						placeholder="请选择角色"
					>
						<el-option
							v-for="(item, i) in RoleList"
							:label="item.Name"
							:value="item.Id"
							:key="i"
						></el-option>
					</el-select>
				</el-form-item>
			</el-form>
			<div slot="footer" class="dialog-footer">
				<el-button @click="editDialogFormVisible = false"
					>取 消</el-button
				>
				<el-button type="primary" @click="EditConfirm()"
					>确 定</el-button
				>
			</div>
		</el-dialog>
	</div>
</template>
<script>
import {
	SelectUser,
	AddUser,
	EditUser,
	DeleteUser,
	SelectALLRole,
} from '../../api/api';
export default {
	data() {
		return {
			tableData: [], //表数据
			queryStr: '',
			currentPage: 1, //当前是第几页
			pageSize: 7,
			pageSizes: [7, 10, 20, 30],
			totalNumber: 0, //共计多少条数据
			orderBy: '',
			desc: true, //正序倒序
			addDialogFormVisible: false,
			editDialogFormVisible: false,
			addForm: {
				UserName: '',
				Password: '',
				RoleId: '',
			},
			editForm: {
				Id: '',
				UserName: '',
				Password: '',
				RoleId: '',
			},
			deleteButtonDisabled: false,
			addButtonDisabled: false,
			formLabelWidth: '120px',
			Query: [
				{
					QueryField: 'Id',
					QueryStr: this.queryStr,
				},
			],
			Orderby: [
				{
					SortField: 'CreateTime',
					IsDesc: false,
				},
			],
			RoleList: [],
		};
	},
	mounted() {
		this.getTableData();
	},
	methods: {
		//获取表格数据
		getTableData() {
			var pageData = {
				Query: this.Query,
				Orderby: this.Orderby,
				CurrentPage: this.currentPage - 1,
				PageNumber: this.pageSize,
			};
			SelectUser(pageData).then((res) => {
				this.tableData = res.data;
				this.totalNumber = res.count;
			});
			SelectALLRole(pageData).then((res) => {
				this.RoleList = res;
			});
		},
		//切换表格每页显示条数
		handleSizeChange(val) {
			this.pageSize = val;
			this.getTableData();
			console.log(`每页 ${val} 条`);
		},
		//切换表格当前页
		handleCurrentChange(val) {
			this.currentPage = val;
			this.getTableData();
			console.log(`当前页: ${val}`);
		},
		//刷新表格
		refreshTable() {
			this.getTableData();
		},
		//查询
		query() {
			this.getTableData();
		},
		//点击添加按钮（改变添加dialog状态）
		changeDialogFormVisible() {
			this.addDialogFormVisible = true;
		},
		//点击确认添加按钮
		AddConfirm() {
			this.addButtonDisabled = true;
			AddUser(this.addForm).then((res) => {
				console.log(res);
				this.addDialogFormVisible = false;
				this.addButtonDisabled = false;
				this.getTableData();
			});
		},
		//点击修改按钮
		handleEdit(row) {
			this.editForm.Id = row.Id;
			this.editForm.UserName = row.UserName;
			this.editForm.Password = row.Password;
			this.editForm.RoleId = row.RoleId;
			this.editDialogFormVisible = true;
		},
		//点击确认修改按钮
		EditConfirm() {
			EditUser(this.editForm).then((res) => {
				console.log(res);
				this.editDialogFormVisible = false;
				this.getTableData();
			});
		},
		//点击删除按钮
		handleDelete(row) {
			this.deleteButtonDisabled = true;
			var Data = {
				id: row.Id,
			};
			console.log(row);
			DeleteUser(Data).then((res) => {
				console.log(res);
				this.deleteButtonDisabled = false;
				this.getTableData();
			});
		},
		sortChange(column) {
			let index = this.Orderby.findIndex(function(value, index, arr) {
				return value.SortField == column.prop;
			});
			console.log(index);

			if (column.order == null) {
				if (index != -1) {
					this.Orderby.splice(index, 1);
				}
				this.getTableData();
				return;
			}
			let desc = false;
			if (column.order == 'ascending') {
				desc = false;
			} else {
				desc = true;
			}

			if (index == -1) {
				this.Orderby.push({
					SortField: column.prop,
					IsDesc: desc,
				});
			} else {
				this.Orderby[index].SortField = column.prop;
				this.Orderby[index].IsDesc = desc;
			}
			console.log(column.order);

			this.getTableData();
		},
	},
};
</script>
