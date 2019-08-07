<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>{{$t('Roles')}}</v-toolbar-title>
            <v-divider class="mx-2"
                       inset
                       vertical>
            </v-divider>
            <v-spacer></v-spacer>
            <v-text-field v-model="search"
                          append-icon="mdi-magnify"
                          :label="$t('Search')"
                          single-line
                          hide-details>
            </v-text-field>
            <v-spacer></v-spacer>
            <v-btn v-if="nucleus.auth.isGranted('Permissions_Role_Create')" @click="editRole()" color="primary" dark class="mb-2">{{$t('NewRole')}}</v-btn>
            <v-dialog v-model="dialog" max-width="500px">
                <v-card>
                    <v-card-title>
                        <span class="headline">{{ formTitle }}</span>
                    </v-card-title>

                    <v-card-text>
                        <div v-for="error in errors" :key="error.name">
                            <v-alert :value="true" type="error">
                                {{$t(error.name)}}
                            </v-alert>
                        </div>
                        <v-form ref="form" @submit.prevent="save">
                            <v-text-field v-model="createOrUpdateRoleInput.role.name"
                                          :label="$t('RoleName')"
                                          :rules="[requiredError]"></v-text-field>
                            <v-list dense>
                                <v-list-item v-for="item in allPermissions" :key="item.id">
                                    <v-checkbox v-model="createOrUpdateRoleInput.grantedPermissionIds" :label="item.displayName" :value="item.id"></v-checkbox>
                                </v-list-item>
                            </v-list>
                        </v-form>
                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" text @click="dialog = false">{{$t('Cancel')}}</v-btn>
                        <v-btn color="blue darken-1" text @click="save">{{$t('Save')}}</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-toolbar>

        <v-data-table :headers="headers"
                      :items="pagedListOfRoleListDto.items"
                      :options.sync="options"
                      :server-items-length="pagedListOfRoleListDto.totalCount"
                      :loading="loading"
                      class="elevation-1">
            <template v-slot:item.action="{ item }">
                <v-icon v-if="!item.isSystemDefault && nucleus.auth.isGranted('Permissions_Role_Update')"
                        small
                        class="mr-2"
                        @click="editRole(item.id)">
                    mdi-pencil
                </v-icon>
                <v-icon v-if="!item.isSystemDefault && nucleus.auth.isGranted('Permissions_Role_Delete')"
                        small
                        @click="deleteRole(item.id)">
                    mdi-delete
                </v-icon>
            </template>
            <template slot="no-data" v-if="!loading">
                <v-alert :value="true" color="error" icon="warning">
                    {{$t('NothingToDisplay')}}
                </v-alert>
            </template>
        </v-data-table>
    </div>
</template>

<script src="./role-list.ts"></script>
