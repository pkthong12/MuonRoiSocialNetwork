﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectVN.Social_Network.Users
{
    public enum EnumUserErrorCodes
    {
        /// <summary>
        /// Mật khẩu không hợp lệ
        /// </summary>
        USR01C,

        /// <summary>
        /// Tài khoản không tồn tại
        /// </summary>
        USR02C,

        /// <summary>
        /// Tên người dùng bắt buộc phải nhập
        /// </summary>
        USR03C,

        /// <summary>
        /// Tên người dùng tối đa 100 kí tự
        /// </summary>
        USR08C,

        /// <summary>
        /// Họ và tên đệm bắt buộc phải nhập
        /// </summary>
        USR04C,

        /// <summary>
        /// Họ và tên đệm tối đa 100 kí tự
        /// </summary>
        USR09C,

        /// <summary>
        /// Tài khoản bắt buộc phải nhập
        /// </summary>
        USR05C,

        /// <summary>
        /// Tài khoản tối đa 100 kí tự
        /// </summary>
        USR10C,

        /// <summary>
        /// Mật khẩu bắt buộc phải nhập
        /// </summary>
        USR06C,

        /// <summary>
        /// Mật khẩu tối đa 1000
        /// </summary>
        USR11C,

        /// <summary>
        /// Key mã hóa bắt buộc phải nhập
        /// </summary>
        USR07C,

        /// <summary>
        /// Key mã hóa tối đa 1000 kí tự
        /// </summary>
        USR12C,

        /// <summary>
        /// Tài khoản đã tồn tại
        /// </summary>
        USR13C,

        /// <summary>
        /// Tài khoản chỉ cho phép [a-z,A-Z,0-9,_]
        /// </summary>
        USR14C,

        /// <summary>
        /// Tài khoản tối thiểu 4 kí tự
        /// </summary>
        USR15C,

        /// <summary>
        /// • Ít nhất một chữ cái viết hoa • ít nhất một chữ cái viết thường • Ít nhất một chữ số • Ít nhất một ký tự đặc biệt
        /// </summary>
        USR17C,

        /// <summary>
        /// Tài khoản tối thiểu 8 kí tự
        /// </summary>
        USR16C,

        /// <summary>
        /// Địa chỉ không quá 1000 kí tự
        /// </summary>
        USR18C,
        /// <summary>
        /// Địa chỉ không được trống
        /// </summary>
        USR32C,

        /// <summary>
        /// Email không đúng định dạng
        /// </summary>
        USR19C,

        /// <summary>
        /// Email không quá 1000 kí tự
        /// </summary>
        USR20C,
        /// <summary>
        /// Email bắt buộc phải nhập
        /// </summary>
        USR31C,
        /// <summary>
        /// Số điện thoại liên hệ phải lớn hơn hoặc bằng 10 kí tự
        /// </summary>
        USR21C,

        /// <summary>
        /// Trạng thái bắt buộc phải nhập
        /// </summary>
        USR22C,

        /// <summary>
        /// Mã kích hoạt không hợp lệ
        /// </summary>
        USR23C,

        /// <summary>
        /// Mật khẩu mới phải khác mật khẩu cũ
        /// </summary>
        USR24C,

        /// <summary>
        /// Mật khẩu cũ không đúng
        /// </summary>
        USR25C,

        /// <summary>
        /// Mật khẩu tối thiếu 8 kí tự
        /// </summary>
        USR26C,

        /// <summary>
        /// Pagezie không quá 100 dòng
        /// </summary>
        USR27C,

        /// <summary>
        /// Tài khoản bị khóa
        /// </summary>
        USR28C,
        /// <summary>
        /// Có lỗi xảy ra vui lòng thử lại
        /// </summary>
        USR29C,
        /// <summary>
        /// Tối thiểu 3 kí tự
        /// </summary>
        USR30C,
        /// <summary>
        /// Địa chỉ tối thiểu 10 kí tự
        /// </summary>
        USRC33C,
        /// <summary>
        /// Ngày sinh không hợp lệ (tuổi phải lớn hơn 10, năm sinh > 1970)
        /// </summary>
        USRC34C,
        /// <summary>
        /// Giới tính lựa chọn không hợp lệ
        /// </summary>
        USRC35C,
    }
}
