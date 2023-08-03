<script setup lang="ts">
import { ref } from "vue";

const searchValue = ref("");
//
const originList = [
    {
        date: '2016-05-03',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-02',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-04',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-03',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-02',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-04',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-03',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-02',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-04',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-03',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-02',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-04',
        name: 'Tom',
        image: "01.jpeg",
        address: 'No. 189, Grove St, Los Angeles',
    },
];
//
const tableData = ref(originList);
//
const Search = () => {
    if (searchValue.value != "") {
        tableData.value = originList.filter(x => x.name.indexOf(searchValue.value) > -1);
    } else {
        tableData.value = originList
    }
};
//
const handleEdit = (index: number, row: {}) => {
    console.log(index, row);
};
//
const handleDelete = (index: number, row: {}) => {
    console.log(index, row);
};
</script>

<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchValue" placeholder="请输入搜索值" @change="Search"></el-input>
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary">新增</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border row-key="id">
                    <el-table-column type="index" lable="index" width="70"></el-table-column>
                    <el-table-column prop="date" lable="Date" width="180"></el-table-column>
                    <el-table-column prop="name" label="Name" width="180"></el-table-column>
                    <el-table-column prop="image" label="Image" width="80">
                        <template #defult="scope">
                            <el-image style="vertical-align: middle;" :src="scope.row.image"
                                :preview-src-list="['01.jpeg']"></el-image>
                        </template>
                    </el-table-column>
                    <el-table-column prop="address" label="Address"></el-table-column>
                    <el-table-column label="Operations" align="center">
                        <template #default="scope">
                            <el-button size="small" type="info" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </el-col>
        </el-row>
    </el-card>
</template>

<style scoped lang="scss"></style>
