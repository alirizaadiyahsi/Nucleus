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
                    <th>Role Name</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in pagedListOfRoleListDto.items">
                    <td>{{ item.name }}</td>
                </tr>
            </tbody>
        </table>

        <p v-else><em>Loading...</em></p>

        <create-role-modal-component />
    </div>
</template>

<script src="./role-list.ts"></script>
