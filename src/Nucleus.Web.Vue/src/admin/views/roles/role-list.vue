<template>
    <div>
        <h1>
            Roles
            <b-btn v-b-modal.modalCreateRole variant="success" class="float-right">
                Create Role
            </b-btn>
        </h1>

        <table v-if="pagedListOfRoleListDto.totalCount" class="table">
            <thead>
                <tr>
                    <th>Actions</th>
                    <th>Role Name</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in pagedListOfRoleListDto.items">
                    <td><button v-if="!item.isSystemDefault" v-on:click="removeRole(item.id)" class="btn btn-danger btn-sm"><i class="fas fa-trash-alt"></i></button></td>
                    <td>{{ item.name }}</td>
                </tr>
            </tbody>
        </table>

        <p v-else><em>Loading...</em></p>

        <create-or-update-role-modal-component />
    </div>
</template>

<script src="./role-list.ts"></script>
